using System;
using System.Text.RegularExpressions;

namespace PreMatriculasParanoa.Domain.Extensions
{
    public static class ValidateExtension
    {
        #region Valida CNPJ

        public static bool CNPJValido(this string CNPJ)
        {
            if (string.IsNullOrEmpty(CNPJ))
                return false;

            int[] Multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int Soma = 0;
            int RestoDivisao = 0;
            string Digito = "";
            string NovoCNPJ = "";

            CNPJ = CNPJ.Replace(",", "").Replace("-", "").Replace("/", "").Replace(".", "").Replace("_", "").Trim();

            if (CNPJ.Length != 14)
                return false;

            NovoCNPJ = CNPJ.Substring(0, 12);

            for (int i = 0; i < 12; i++)
                Soma += int.Parse(NovoCNPJ[i].ToString()) * Multiplicador1[i];

            RestoDivisao = (Soma % 11);
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = RestoDivisao.ToString();
            NovoCNPJ = NovoCNPJ + Digito;
            Soma = 0;

            for (int pos = 0; pos < 13; pos++)
                Soma = Soma + Convert.ToInt32(NovoCNPJ[pos].ToString()) * Multiplicador2[pos];
            RestoDivisao = (Soma % 11);

            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            NovoCNPJ += RestoDivisao;

            return NovoCNPJ == CNPJ;
        }

        #endregion Valida CNPJ

        #region Valida CPF

        public static bool CPFValido(this string CPF)
        {
            if (string.IsNullOrEmpty(CPF))
                return false;

            int[] Multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] Multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string NovoCPF;
            string Digito = "";
            int Soma = 0;
            int RestoDivisao = 0;

            CPF = CPF.Replace(".", "").Replace("_", "").Replace("-", "").Trim();

            if (CPF.Length != 11)
                return false;

            NovoCPF = CPF.Substring(0, 9);

            for (int i = 0; i < 9; i++)
                Soma += int.Parse(NovoCPF[i].ToString()) * Multiplicador1[i];

            RestoDivisao = Soma % 11;
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = RestoDivisao.ToString();

            NovoCPF = NovoCPF + Digito;

            Soma = 0;
            for (int i = 0; i < 10; i++)
                Soma += int.Parse(NovoCPF[i].ToString()) * Multiplicador2[i];

            RestoDivisao = Soma % 11;
            if (RestoDivisao < 2)
                RestoDivisao = 0;
            else
                RestoDivisao = 11 - RestoDivisao;

            Digito = Digito + RestoDivisao;

            return CPF.EndsWith(Digito);
        }

        public static bool ValidaCpfComNumerosRepetidos(this string cpf)
        {
            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace(".", "");
            Regex reg = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");

            if (!reg.IsMatch(cpf))
                return false;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";
            // Pesos para calcular o primeiro dígito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            // Pesos para calcular o segundo dígito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];
            bool retorno = false;

            // Limpa a string
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
            {
                return false;
            }
            // Caso coloque todos os numeros iguais
            switch (cpf)
            {
                case "00000000000":
                    return false;

                case "11111111111":
                    return false;

                case "22222222222":
                    return false;

                case "33333333333":
                    return false;

                case "44444444444":
                    return false;

                case "55555555555":
                    return false;

                case "66666666666":
                    return false;

                case "77777777777":
                    return false;

                case "88888888888":
                    return false;

                case "99999999999":
                    return false;
            }
            try
            {        // Quebra cada digito do CPF
                n[0] = Convert.ToInt32(cpf.Substring(0, 1)); n[1] = Convert.ToInt32(cpf.Substring(1, 1));
                n[2] = Convert.ToInt32(cpf.Substring(2, 1)); n[3] = Convert.ToInt32(cpf.Substring(3, 1));
                n[4] = Convert.ToInt32(cpf.Substring(4, 1)); n[5] = Convert.ToInt32(cpf.Substring(5, 1));
                n[6] = Convert.ToInt32(cpf.Substring(6, 1)); n[7] = Convert.ToInt32(cpf.Substring(7, 1));
                n[8] = Convert.ToInt32(cpf.Substring(8, 1)); n[9] = Convert.ToInt32(cpf.Substring(9, 1));
                n[10] = Convert.ToInt32(cpf.Substring(10, 1));
            }
            catch
            {
                return false;
            }
            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound(0); i++)
            {
                soma += (peso1[i] * Convert.ToInt32(n[i]));
            }
            // Pega o resto da divisao
            int resto = soma % 11;
            if (resto == 1 || resto == 0)
            {
                d1 = 0;
            }
            else
            {
                d1 = 11 - resto;
            }

            soma = 0;

            // Calcula cada dígito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound(0); i++)
            {
                soma += (peso2[i] * Convert.ToInt32(n[i]));
            }
            // Pega o resto da divisão
            resto = soma % 11;
            if (resto == 1 || resto == 0)
            {
                d2 = 0;
            }
            else
            {
                d2 = 11 - resto;
            }

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            //Se os últimos dois dígitos calculados baterem com os dois últimos dígitos do cpf entao é válido
            if (calculado == digitado)
            {
                retorno = true;
            }
            else
            {
                retorno = false;
            }

            return retorno;
        }

        public static bool ValidarCpfComNumerosRepetidos(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Replace("-", "");
            cpf = cpf.Replace(".", "");
            Regex reg = new Regex(@"(^(\d{3}.\d{3}.\d{3}-\d{2})|(\d{11})$)");

            if (!reg.IsMatch(cpf))
                return false;

            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace("\\", "");

            if (cpf.Length != 11)
                return false;

            switch (cpf)
            {
                case "00000000000":
                case "11111111111":
                case "22222222222":
                case "33333333333":
                case "44444444444":
                case "55555555555":
                case "66666666666":
                case "77777777777":
                case "88888888888":
                case "99999999999":
                    return true;
            }
            return false;
        }

        #endregion Valida CPF
    }
}
