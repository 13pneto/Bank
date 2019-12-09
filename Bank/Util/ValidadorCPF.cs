using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendasConsole.Utils
{
    class ValidadorCPF
    {

        public static bool validaCpf(string cpf)
        {
            if(cpf == null)
            {
                return false;
            }

            if (cpf.Length != 11)
            {
                return false;
               // throw new Exception("CPF deve ter 11 digitos.");
            }

            //----------------------------------------------------DIGITO 1----------------------------------------------------------
            string temp = cpf.Substring(9, 1);
            int digitoInformado1 = int.Parse(temp);
            temp = cpf.Substring(10, 1);
            int digitoInformado2 = int.Parse(temp);

            int[] vet1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0};
            int[] vetCpf = new int[11];
            char[] cpfArray = cpf.ToCharArray();

            int totalDig1 = 0;
            int digito1 = 0;
            int digito2 = 0;

            for (int i = 0; i < 9; i++)
            {
                temp = cpf.Substring(i, 1);
                totalDig1 += vet1[i] * int.Parse(temp);
            }

            int resto = totalDig1 % 11;

            if ((11 - resto) <= 9)
            {
                digito1 = 11 - resto;
            }

            //----------------------------------------------------DIGITO 2----------------------------------------------------------

            int[] vet2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int totalDig2 = 0;
            for (int i = 0; i < 10; i++)
            {
                temp = cpf.Substring(i, 1);
                totalDig2 += vet2[i] * int.Parse(temp);
            }

            resto = 0;
            resto = totalDig2 % 11;

            if ((11 - resto) <= 9)
            {
                digito2 = 11 - resto;
            }

            //----------------------------------------------------VERIFICAÇÃO RESULTADO----------------------------------------------------------

            if (digito1 == digitoInformado1 && digito2 == digitoInformado2)
            {
                return true;
            }
            else
            {
                return false;
                //throw new Exception("Cpf Inválido");
            }
        }
    }
}
