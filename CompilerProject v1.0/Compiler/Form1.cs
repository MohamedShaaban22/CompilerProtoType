using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Compiler
{
    public partial class Form1 : Form
    {
        string[,] Tokens_Types = new string[100,2];
        int index;
        public Form1()
        {
            InitializeComponent();
        }

        private void token_Click(object sender, EventArgs e)
        {
            output_txt.Text = "";
            index = 0;
            string input = input_txt.Text;
            char x;
            string tkn = "";
            for (int i = 0; i < input.Length; i++)
            {
                x = input[i];
                if (x == ' '||x == '\n'||x=='\r')
                    continue;                
                tkn += x;
                if (x == ';'||x==',')
                {
                    output_txt.Text += x + " \t separator" + Environment.NewLine;
                    tkn = "";
                    continue;

                }
                if (x == '"')
                {
                    output_txt.Text += x + " \t punctuation" + Environment.NewLine;
                    tkn = "";

                    while(i+1 != input.Length)
                    {
                        i++;
                        x = input[i];
                        if (x != '"')
                        {
                            tkn += x;
                            
                        }
                        else
                        {
                            output_txt.Text += tkn + " \t string" + Environment.NewLine;
                            output_txt.Text += x + " \t punctuation" + Environment.NewLine;
                            tkn = "";
                            break;
                        }
                    }
                    i++;
                    continue;
                }
                
                if (If_key_word(tkn))
                {
                    if (i + 1 == input.Length)
                    {
                        output_txt.Text += tkn + " \t kew word" + Environment.NewLine;
                        Tokens_Types[index, 0] = tkn;
                        Tokens_Types[index, 1] = "key_word";
                        index++;
                        tkn = "";

                    }
                    else
                    {
                        if (input[i + 1] == ' ' || If_operator(input[i + 1].ToString()) )
                        {
                            output_txt.Text += tkn + " \t kew word" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "key_word";
                            index++;
                            tkn = "";

                        }
                        
                    }

                }

                else if (If_identifier(tkn))
                {
                   
                    if (i + 1 == input.Length)
                    {
                        output_txt.Text += tkn + " \t identifier" + Environment.NewLine;
                        Tokens_Types[index, 0] = tkn;
                        Tokens_Types[index, 1] = "identifier";
                        index++;
                        tkn = "";
                    }
                    else
                    {
                        if (input[i + 1] == ' '|| input[i + 1] == '\r'|| input[i + 1] == '\n')
                        {
                            output_txt.Text += tkn + " \t identifier" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "identifier";
                            index++;
                            tkn = "";
                            i++;
                        }
                        else
                        {
                            string str;
                            str = tkn + input[i+1];

                            if (!If_identifier(str))
                            {
                                output_txt.Text += tkn + " \t identifier" + Environment.NewLine;
                                Tokens_Types[index, 0] = tkn;
                                Tokens_Types[index, 1] = "identifier";
                                index++;
                                tkn = "";
                            }
                            
                        }
                        
                        
                    }

                    

                }
                
                else if (If_number(tkn))
                {
                    
                    if (i + 1 != input.Length)
                    {
                        string str = tkn + input[i+1];
                        if (input[i + 1] == ' ' || input[i + 1] == '\r'|| input[i + 1] == '\n')
                        {
                            output_txt.Text += tkn + " \t number" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "number";
                            index++;
                            tkn = "";
                        }
                        else if (If_number(str))
                        {
                            continue;
                        }
                        else
                        {
                            output_txt.Text += tkn + " \t number" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "number";
                            index++;
                            tkn = "";
                        }
                    }
                    else
                    {
                            output_txt.Text += tkn + " \t number" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "number";
                            index++;
                            tkn = "";
                        
                    }

                    
                }

                else if (If_operator(tkn))
                {

                    int o = i;
                    if (o + 1 != input.Length)
                    {

                        string str = tkn;
                        str = tkn + input[o+1];
                        if (tkn == "." && If_number(str) && output_txt.Text[(output_txt.Text.Length) - 6] != 'f')
                            continue;
                        else if (!If_operator(str)|| input[++o] == ' '|| input[++o] == '\r'|| input[++o] == '\n')
                        {
                            output_txt.Text += tkn + " \t operator" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "operator";
                            index++;
                            tkn = "";
                        }
                    }
                    else
                    {
                            output_txt.Text += tkn + " \t operator" + Environment.NewLine;
                            Tokens_Types[index, 0] = tkn;
                            Tokens_Types[index, 1] = "operator";
                            index++;
                            tkn = "";
                        
                    }
                    
                }

                else 
                {
                    output_txt.Text += tkn +" \t Error!!!!" + Environment.NewLine;
                    Tokens_Types[index, 0] = tkn;
                    Tokens_Types[index, 1] = "Error!!!!!";
                    index++;
                    tkn = "";
                }
            }
            
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            output_l_text.Text = "";
            bool s = false , n = false, t=false;
            try
            {
                for (int i=0;i<index;)
                {
                    if (Tokens_Types[i, 1] == "key_word")
                    {
                        t = false;
                        if (Tokens_Types[i, 0] == "ادخل")
                        {
                            if (Tokens_Types[i + 1, 1] == "identifier")
                            {
                                output_l_text.Text += "جمله ادخال صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                            else
                            {
                                output_l_text.Text += "جمله ادخال غير صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                        }

                        else if (Tokens_Types[i, 0] == "اطبع")
                        {
                            if (Tokens_Types[i + 1, 1] == "identifier" || Tokens_Types[i + 1, 1] == "number")
                            {
                                output_l_text.Text += "جمله طباعه صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                            else
                            {
                                output_l_text.Text += "جمله طباعه غير صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                        }

                        else if (Tokens_Types[i, 0] == "منطفي" || Tokens_Types[i, 0] == "صحيح" || Tokens_Types[i, 0] == "نصي")
                        {
                            if (Tokens_Types[i + 1, 1] == "identifier")
                            {
                                output_l_text.Text += "جمله تعريف صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                            else
                            {
                                output_l_text.Text += "جمله تعريف غير صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                        }
                        else if (Tokens_Types[i, 0] == "اذا")
                        {
                          
                            if (Tokens_Types[i + 1, 0] == "(" && Tokens_Types[i + 3, 1] == "operator" &&  ( Tokens_Types[i + 2, 1] == "number"|| Tokens_Types[i + 2, 1] == "identifier")  && (Tokens_Types[i + 4, 1] == "identifier" || Tokens_Types[i + 4, 1] == "number") && Tokens_Types[i + 5, 0] == ")" && Tokens_Types[i + 6, 0] == "نفذ")                        
                            {
                                output_l_text.Text += "جمله شرطيه صحيحه" + Environment.NewLine;
                                i += 7;

                            }
                            else if(Tokens_Types[i + 1, 0] == "(" && Tokens_Types[i + 2, 1] == "identifier" && Tokens_Types[i + 3, 0] == ")" && Tokens_Types[i + 4, 0] == "نفذ")
                            {
                                output_l_text.Text += "جمله شرطيه صحيحه" + Environment.NewLine;
                                i += 5;
                            }
                            else
                            {
                                output_l_text.Text += "جمله شرطيه غير صحيحه" + Environment.NewLine;
                                i++;
                            }
                        }
                        else if (Tokens_Types[i, 0] == "نفذ")
                        {
                            output_l_text.Text += "جمله  خاطئه" + Environment.NewLine;
                            i++;
                        }

                        else if (Tokens_Types[i, 0] == "من")
                        {

                            if (Tokens_Types[i + 1, 1] == "identifier" && (Tokens_Types[i + 2, 0] == "=" && Tokens_Types[i + 3, 1] == "number") && (Tokens_Types[i + 4, 0] == "حتي" && ( Tokens_Types[i + 5, 1] == "number") || Tokens_Types[i + 5, 1] == "identifier") && Tokens_Types[i + 6, 0] == "خطوه")                     
                            {
                                output_l_text.Text += "جمله تكرار صحيحه" + Environment.NewLine;
                                i += 6;

                            }
                            else if ((Tokens_Types[i + 1, 1] == "identifier" || Tokens_Types[i + 1, 1] == "number") && Tokens_Types[i + 2, 0] == "حتي" && (Tokens_Types[i + 3, 1] == "identifier" || Tokens_Types[i + 3, 1] == "number") && Tokens_Types[i + 4, 0] == "خطوه")
                            {
                                output_l_text.Text += "جمله تكرار صحيحه" + Environment.NewLine;
                                i += 5;
                            }
                            else
                            {
                                output_l_text.Text += "جمله تكرار غير صحيحه" + Environment.NewLine;
                                i++;
                            }
                        }
                        else if (Tokens_Types[i, 0] == "خطوه")
                        {
                            output_l_text.Text += "جمله  خاطئه" + Environment.NewLine;
                            i++;
                        }
                        else if (Tokens_Types[i, 0] == "حتي")
                        {
                            output_l_text.Text += "جمله  خاطئه" + Environment.NewLine;
                            i++;
                        }

                    }
                    
                    else if (Tokens_Types[i, 1] == "identifier")
                    {
                        t = false;
                        if (s)
                        {
                            output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                            i++;
                            continue;
                        }
                        else
                        {
                            if (n)
                            {
                                output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                                n = false;
                            }
                            i++;
                            s = true;
                            continue;
                        }

                    }
                    
                    else if (Tokens_Types[i, 1] == "operator")
                    {
                        if (Tokens_Types[i, 0] == "=")
                        {
                            if ((Tokens_Types[i + 1, 1] == "identifier" || Tokens_Types[i + 1, 1] == "number") && (Tokens_Types[i - 1, 1] == "identifier"))
                            {
                                output_l_text.Text += "جمله اسناد صحيحه" + Environment.NewLine;
                                i += 2;
                                t = true;
                            }
                            else
                            {
                                output_l_text.Text += "جمله اسناد غير صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                        }

                        else if (Tokens_Types[i, 0] == ".")
                        {
                            if ((Tokens_Types[i + 1, 1] == "identifier" || Tokens_Types[i + 1, 1] == "number") && (Tokens_Types[i - 1, 1] == "identifier"))
                            {
                                output_l_text.Text += "جمله خواص صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                            else
                            {
                                output_l_text.Text += "جمله خواص غير صحيحه" + Environment.NewLine;
                                i += 2;
                            }
                        }

                        else
                        {
                            if ((Tokens_Types[i + 1, 1] == "identifier" || Tokens_Types[i + 1, 1] == "number") && (Tokens_Types[i - 1, 1] == "identifier" || Tokens_Types[i - 1, 1] == "number") && Tokens_Types[i - 2, 0] == "="  && t == true)
                            {
                                i += 2;
                            }
                            else
                            {
                                output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                                i += 2;
                            }
                        }
                    }
                    
                    else if (Tokens_Types[i, 1] == "number")
                    {
                        t = false;
                        if (n)
                        {
                            output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                            i++;
                            continue;
                        }
                        else
                        {
                            if(s)
                            {
                               output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                                s = false;
                            }
                            i++;
                            n = true;
                            continue;
                        }
                    }
                    s = false;
                    n = false;
                }
                if(s==true||n==true)
                {
                    output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
                }
                
            }
            catch (Exception)
            {
                output_l_text.Text += "جمله خاطئه" + Environment.NewLine;
            }
        }

        public bool If_number(string token)
        {
            if (Int32.TryParse(token, out int x))
            {
                return true;
            }
            else if (float.TryParse(token, out float y))
            {
                return true;
            }
            else
                return false;
        }
        public bool If_key_word(string token)
        {
            string[] kew_word = { "اذا", "ادخل", "اطبع", "من", "حتي", "صحيح", "نصي", "منطقي", "خطوه", "نفذ" };
            if (kew_word.Contains(token))
                return true;
            else
                return false;


        }
        public bool If_identifier(string token)
        {
            char[] begin =  {'_','ا','ب','ت','ث','ج','ح','خ','د','ذ','ر','ز','س','ش','ص','ض','ط','ظ','ع','غ','ف','ق','ك','ل','م','ن','ه',
                'و', 'ي', 'ئ', 'ء', 'ؤ', 'ه', 'ة', 'ى'};
            char[] num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };

            if (!begin.Contains(token[0]))
            {
                return false;
            }
            for (int i = 0; i < token.Length; i++)
            {
                if (begin.Contains(token[i]) || num.Contains(token[i]))
                {
                    continue;
                }
                else
                    return false;

            }
            return true;
        }
        public bool If_operator(string token)
        {
            string[] operators = { ".", "=", "==", "<=", ">=", "<>", "><", ">", "<", "/", "*", "-", "+", "%", "+=", "-=", "*=", "//=", ")", "(", "++", "--", "!=", "!" };
            if (operators.Contains(token))
                return true;
            else
                return false;


        }

    }
}