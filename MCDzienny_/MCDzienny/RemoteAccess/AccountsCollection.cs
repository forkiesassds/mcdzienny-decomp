using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace MCDzienny.RemoteAccess
{
    [Serializable]
    public class AccountsCollection : Dictionary<string, string>, IDisposable
    {

        readonly ASCIIEncoding asci = new ASCIIEncoding();
        readonly MD5CryptoServiceProvider md5crypto = new MD5CryptoServiceProvider();

        bool disposed;

        public void Dispose()
        {
            Dispose(fromDispose: true);
            GC.SuppressFinalize(this);
        }

        public event EventHandler ElementChanged;

        protected virtual void OnElementChanged(EventArgs args)
        {
            if (ElementChanged != null)
            {
                ElementChanged(this, args);
            }
        }

        public new void Clear()
        {
            base.Clear();
            OnElementChanged(EventArgs.Empty);
        }

        public new void Remove(string key)
        {
            base.Remove(key);
            OnElementChanged(EventArgs.Empty);
        }

        public new void Add(string login, string plainPassword)
        {
            base.Add(login, EncryptPassword(plainPassword));
            OnElementChanged(EventArgs.Empty);
        }

        public void AddEncrypted(string login, string encryptedPassword)
        {
            base.Add(login, encryptedPassword);
            OnElementChanged(EventArgs.Empty);
        }

        public void ChangePassword(string login, string newPassword)
        {
            base[login] = EncryptPassword(newPassword);
        }

        string EncryptPassword(string password)
        {
            return BitConverter.ToString(md5crypto.ComputeHash(asci.GetBytes(password))).Replace("-", "").TrimStart('0')
                .ToLower();
        }

        protected virtual void Dispose(bool fromDispose)
        {
            if (!disposed)
            {
                if (md5crypto != null)
                {
                    md5crypto.Clear();
                }
                disposed = true;
            }
        }

        ~AccountsCollection()
        {
            Dispose(fromDispose: false);
        }
    }
}