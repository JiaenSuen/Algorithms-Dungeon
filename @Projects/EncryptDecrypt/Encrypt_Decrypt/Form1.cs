using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using Encrypt_Decrypt;   


namespace Encrypt_Decrypt {   


    public partial class Form1 : Form
    {
        private readonly Dictionary<string, Func<string, string>> _encryptMap;
        private readonly Dictionary<string, Func<string, string>> _decryptMap;

        public List<string> Encrypt_List;

        public Form1() {
            InitializeComponent();

            Encrypt_List = new List<string>();

            _encryptMap = new Dictionary<string, Func<string, string>>  
            {
                { "Excess-3"     , CryptoUtils.Simple.Encrypt_Caesar3    },
                { "Vigenere-KEY" , CryptoUtils.Simple.Encrypt_Vigenere   },
                { "XOR-K"        , CryptoUtils.Simple.Encrypt_XOR        },
                { "XOR-64-K"     , CryptoUtils.Simple.Encrypt_XOR_Base64 },
                { "Feistel"      , CryptoUtils.Simple.Encrypt_Feistel    }, // Encrypt_TripleDES_CBC
                { "Playfair"      , CryptoUtils.Simple.Encrypt_Playfair  },

                { "DES"     , CryptoUtils.Symmetric.Encrypt_DES             },
                { "3DES"    , CryptoUtils.Symmetric.Encrypt_TripleDES       },
                { "3DES-CBC", CryptoUtils.Symmetric.Encrypt_TripleDES_CBC   },
                { "AES"     , CryptoUtils.Symmetric.Encrypt_AES             },
                { "RC2"     , CryptoUtils.Symmetric.Encrypt_RC2             },

                { "RSA" , CryptoUtils.Asymmetric.Encrypt_RSA            }
            };

 
            _decryptMap = new Dictionary<string, Func<string, string>>  
            {
                { "Excess-3"     , CryptoUtils.Simple.Decrypt_Caesar3    },
                { "Vigenere-KEY" , CryptoUtils.Simple.Decrypt_Vigenere   },
                { "XOR-K"        , CryptoUtils.Simple.Encrypt_XOR        },
                { "XOR-64-K"     , CryptoUtils.Simple.Decrypt_XOR_Base64 },
                { "Feistel"      , CryptoUtils.Simple.Decrypt_Feistel    },
                { "Playfair"     , CryptoUtils.Simple.Decrypt_Playfair  },

                { "DES"     , CryptoUtils.Symmetric.Decrypt_DES           },
                { "3DES"    , CryptoUtils.Symmetric.Decrypt_TripleDES     },
                { "3DES-CBC", CryptoUtils.Symmetric.Decrypt_TripleDES_CBC },
                { "AES"     , CryptoUtils.Symmetric.Decrypt_AES           },
                { "RC2"     , CryptoUtils.Symmetric.Decrypt_RC2           },

                { "RSA" , CryptoUtils.Asymmetric.Decrypt_RSA         }
            };

        }


       
        private void EncryBtn_Click(object sender, EventArgs e){
            string result = text_val.Text;
            Encrypt_Procedure_Text.Text = "";
            Encrypt_List.Clear();
 
            foreach (var item in Selection_Encrypto_List.Items)
            {
                string key = item.ToString();
                if (_encryptMap.ContainsKey(key)) {
                    result = _encryptMap[key](result);
                    Encrypt_Procedure_Text.Text += key + " : " + result + "\r\n";
                    Encrypt_List.Add(key);
                }
                else MessageBox.Show($"Encryption method not found：{key}");
                
            }

             
            txtEncrypt.Text = result;
        }

        private void DecryBtn_Click(object sender, EventArgs e)
        {
            string result = txtEncrypt.Text;
            Decrypt_Procedure_Text.Text = "";

            // Reverse execution decryption
            for (int i = Encrypt_List.Count - 1; i >= 0; i--)
            {
                string key = Encrypt_List[i];
                if (_decryptMap.ContainsKey(key)) {
                    result = _decryptMap[key](result);
                    Decrypt_Procedure_Text.Text += key + " : " + result + "\r\n";
                }
                else MessageBox.Show($"Decryption method not found：{key}");
 
            }

            text_Decrypt.Text = result;
        }

        private void btnAddToSequence_Click(object sender, EventArgs e)
        {
            Selection_Encrypto_List.Items.Clear();
            foreach (var item in Selection_Encrpto_checkedListBox.CheckedItems)
            {    
                if (!Selection_Encrypto_List.Items.Contains(item)){
                    Selection_Encrypto_List.Items.Add(item);
                }
            }
        }

        private void btnMoveUp_Click_Click(object sender, EventArgs e)
        {
            int index = Selection_Encrypto_List.SelectedIndex;
            if (index > 0)
            {
                var item = Selection_Encrypto_List.Items[index];
                Selection_Encrypto_List.Items.RemoveAt(index);
                Selection_Encrypto_List.Items.Insert(index - 1, item);
                Selection_Encrypto_List.SelectedIndex = index - 1;
            }
        }

        private void btnMoveDown_Click_Click(object sender, EventArgs e)
        {
            int index = Selection_Encrypto_List.SelectedIndex;
            if (index >= 0 && index < Selection_Encrypto_List.Items.Count - 1)
            {
                var item = Selection_Encrypto_List.Items[index];
                Selection_Encrypto_List.Items.RemoveAt(index);
                Selection_Encrypto_List.Items.Insert(index + 1, item);
                Selection_Encrypto_List.SelectedIndex = index + 1;
            }
        }
    }
}
