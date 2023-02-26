using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CALCULADORA_CIENTÍFICA
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string expressao = textBox1.Text;
            textBox2.Text = Parenteses("("+expressao+")");

        }
        
        string operadores = "^*/+-";


        public string Calcular(string expressao,int controlaTipo)
        {

            int i=0,j=0, contadorDeOperadores = 0;

            for ( i = 0; i < expressao.Length; i++)
            {
                for (j = 0; j < operadores.Length; j++)
                    if (expressao[i] == operadores[j])
                        contadorDeOperadores++;
            }

            switch (contadorDeOperadores)
            {
                case 0: return expressao;
                case 1: return Case1(expressao);
                default: return "(" + CaseDefault_PorParenteses(expressao,controlaTipo) + ")";
                          
            }
            

        }

        public string CaseDefault_PorParenteses(string expressao,int controlaTipo)
        {
            int i, j, l1, l2, opl1=-1, opl2=-1, opPrioridade=-1, tamanhoEx = expressao.Length , tamanhoOp = operadores.Length;
            int controlador = 0;


            for (j = 0; j < tamanhoOp; j++)
            {
                if (controlador == 2)
                    break;
                for (i = 0; i < tamanhoEx; i++)
                    if (operadores[j] == expressao[i])
                    {
                        opPrioridade = i;


                        for (l1 = i - 1; l1 > -1; l1--)
                        {
                            if (operadores.Contains(expressao[l1]))
                            {
                                opl1 = l1;
                                break;
                            }

                        }


                        for (l2 = i + 1; l2 < tamanhoEx; l2++)
                        {
                            if (operadores.Contains(expressao[l2]))
                            {
                                opl2 = l2;
                                break;
                            }

                        }

                        controlador = 2;
                        break;

                    }
            }

            if (opPrioridade != -1)
            {
                if (opl1 == -1)
                    if (opl2 == -1)
                        return expressao;
                    else
                        return "(" + expressao.Substring(0, opl2) + ")" + expressao.Substring(opl2);
                else
                  if (opl2 == -1)


                    if(controlaTipo==1)
                        return "(" + expressao.Substring(0, opl1 + 1) + "(" + expressao.Substring(opl1 + 1) + ")" + ")";
                   else
                       return expressao.Substring(0, opl1 + 1) + "(" + expressao.Substring(opl1 + 1) + ")";
                
                else

                    if (controlaTipo == 1)
                    return "(" + expressao.Substring(0, opl1 + 1) + "(" + expressao.Substring(opl1 + 1, (opl2 - opl1) - 1) + ")" + expressao.Substring(opl2) + ")";

                else
                return expressao.Substring(0, opl1 + 1) + "(" + expressao.Substring(opl1 + 1, (opl2 - opl1) - 1) + ")" + expressao.Substring(opl2);

            }

            else

                return expressao;
        }
    
        public string Case1(string expressao)
        {

            int i = 0, tamanho = expressao.Length;

            double num1 = 0, resultado = 0;
            double num2 = 0;
            string op = string.Empty;


            for (i = 0; i < expressao.Length; i++)

                if ((expressao[i] == '^') || (expressao[i] == '+') || (expressao[i] == '-') || (expressao[i] == '*') || (expressao[i] == '/'))
                {
                    op += expressao[i];
                    num2 = double.Parse( expressao.Substring(i + 1) );
                    num1 = double.Parse( expressao.Substring(0 , i) );
                }


            if (op != string.Empty)
            {
                switch (op)

                {
                    case "^": resultado = Math.Pow(num1,num2) ; break;
                    case "+": resultado = num1 + num2; break;
                    case "-": resultado = num1 - num2; break;
                    case "*": resultado = num1 * num2; break;
                    case "/": resultado = num1 / num2; break;
                }
                
                return resultado.ToString();
            }
            else

                return expressao;

        }

            public string Parenteses(string expressao)
            {
            int controlaTipo = 0;
                int i = 0, p1 = -1, p2 = -1;
                string novastring=string.Empty;

                for(; ; )
                {
                    for (i = 0; i < expressao.Length; i++)
                    {
                        if (expressao[i] == '(')
                            p1 = i;
        
                    
                    if (expressao[i] == ')')
                        {
                            p2 = i;
                            controlaTipo = 1;
                            if (p1 == -1)
                            {
                                p1 = 0;
                                novastring = "(" + expressao;
                                expressao = string.Empty;
                                expressao = novastring;
                                novastring = string.Empty;
                                break;
                            }
                            novastring += expressao.Substring(0, p1);
                            novastring += Calcular(expressao.Substring(p1 + 1, (p2 - p1) - 1), controlaTipo);
                            novastring += expressao.Substring(p2 + 1);
                            expressao = string.Empty;
                            expressao = novastring;
                            novastring = string.Empty;
                            controlaTipo = 0;
                            p1 = -1;
                            p2 = -1;
                            break;
                        }
                        if (i + 1 == expressao.Length)
                        
                            return Calcular(expressao, controlaTipo);
                    }
                }
            

            }
        
        }
    }

