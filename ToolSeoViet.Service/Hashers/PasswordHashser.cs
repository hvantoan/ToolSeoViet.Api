﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace ToolSeoViet.Services.Hashers {

    public static class PasswordHashser {
        private const int saltSize = 32;

        public static string Hash(string password) {
            byte[] salt = GenerateSalt();
            return HashWithSalt(password, salt);
        }

        public static bool Verify(string password, string passwordStored) {
            byte[] salt = GetSaltFromPasswordStored(passwordStored);
            string passwordHashed = HashWithSalt(password, salt);
            return passwordHashed.Equals(passwordStored);
        }

        private static string HashWithSalt(string password, byte[] salt) {
            byte[] pw = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 10000, 32);
            byte[] stored = MergeSaltAndPassword(salt, pw);
            return Convert.ToBase64String(stored);
        }

        private static byte[] GenerateSalt() {
            byte[] salt = new byte[saltSize];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }

        private static byte[] GetSaltFromPasswordStored(string passwordStored) {
            byte[] stored = Convert.FromBase64String(passwordStored);
            byte[] salt = new byte[saltSize];
            Array.Copy(stored, 0, salt, 0, saltSize);
            return salt;
        }

        private static byte[] MergeSaltAndPassword(byte[] salt, byte[] pw) {
            byte[] store = new byte[salt.Length + pw.Length];
            salt.CopyTo(store, 0);
            pw.CopyTo(store, salt.Length);
            return store;
        }
    }
}