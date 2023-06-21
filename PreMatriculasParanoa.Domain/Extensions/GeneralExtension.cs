using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace PreMatriculasParanoa.Domain.Extensions
{
    public static class GeneralExtension
    {
        public static string ToDateSqlCast(this DateTime dt)
        {
            return $@"CAST('{dt.ToSqlDateTimeString()}' AS DATETIME)";
        }

        public static string ToDateSqlCast(this string dt)
        {
            return $@"CAST('{Convert.ToDateTime(dt).ToSqlDateTimeString()}' AS DATETIME)";
        }

        private static readonly string[] meses = "Janeiro,Fevereiro,Março,Abril,Maio,Junho,Julho,Agosto,Setembro,Outubro,Novembro,Dezembro".Split(',');

        ///<summary>
        ///Retorna a última hora do dia informado.
        ///Exemplo: 31/03/2011 como parâmetro retorna 31/03/2011 23:59:59
        ///</summary>
        ///<param name="DataInicio">Data Inicial</param>
        ///<returns></returns>
        public static DateTime ToUltimaHoraData(this DateTime DataInicio)
        {
            DateTime dtInicio = DataInicio;
            DateTime dtFim = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, 23, 59, 59);
            return dtFim;
        }

        ///<summary>
        ///Retorna a primeira hora do dia informado.
        ///Exemplo: 31/03/2011 como parâmetro retorna 31/03/2011 00:00:00
        ///</summary>
        ///<param name="DataInicio">Data Inicial</param>
        ///<returns></returns>
        public static DateTime ToPrimeiraHoraData(this DateTime DataInicio)
        {
            DateTime dtInicio = DataInicio;
            DateTime dtFim = new DateTime(dtInicio.Year, dtInicio.Month, dtInicio.Day, 0, 0, 0);
            return dtFim;
        }

        public static bool IsDayWeekend(this DateTime valor)
        {
            return (valor.DayOfWeek == DayOfWeek.Saturday || valor.DayOfWeek == DayOfWeek.Sunday);
        }

        #region GetMonthName

        public static string GetMonthName(this int valor)
        {
            if (valor >= 1 && valor <= 12)
            {
                CultureInfo culture = new CultureInfo("pt-BR");
                DateTimeFormatInfo dtfi = culture.DateTimeFormat;
                return culture.TextInfo.ToTitleCase(dtfi.GetMonthName(valor));
            }

            return "Não se aplica";
        }

        #endregion GetMonthName

        #region Between

        /// <summary>
        /// Compara se um valor se econtra dentro de um intervalo
        /// </summary>
        /// <typeparam name="T">Tipo a ser comparado</typeparam>
        /// <param name="value">Valor a ser comparado</param>
        /// <param name="MenorValor">Menor valor</param>
        /// <param name="MaiorValor">Maio valor</param>
        /// <returns>Verdadeiro quando o valor estiver dentro do intervalor</returns>
        public static bool Between<T>(this T value, T MenorValor, T MaiorValor) where T : IComparable<T>
        {
            if (MenorValor.CompareTo(MaiorValor) > 0)
                return false;

            return (value.CompareTo(MenorValor) >= 0 && value.CompareTo(MaiorValor) <= 0);
        }

        #endregion Between

        #region ToTenths

        /// <summary>
        /// TenthsOfAMilimeter é a medida utilizada nos relatórios.
        /// 1 TenthsOfAMilimeter equivale à décima parte de um milímetro.
        /// </summary>
        /// <param name="value">Valor em centrimetros</param>
        /// <returns>Valor em Tenths</returns>
        public static int ToTenths(this decimal value)
        {
            return (int)Math.Round(value * 100);
        }

        /// <summary>
        /// Para transformar de TenthsOfAMilimeter para Centímetros, deve-se dividir o valor por 100
        /// </summary>
        /// <param name="value">Valor em Tenths</param>
        /// <returns>Valore em centrimetros</returns>
        public static decimal FromTenths(this int value)
        {
            return (decimal)value / 100;
        }

        #endregion ToTenths

        #region ToInt32

        public static int ToInt32(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            int resultado = 0;
            int.TryParse(valor, out resultado);
            return resultado;
        }

        public static int ToInt32(this object valor)
        {
            if (valor == null)
                return 0;

            int resultado = 0;
            try
            {
                resultado = (int)valor;
            }
            catch (Exception)
            {
                int.TryParse(valor.ToString(), out resultado);
            }

            return resultado;
        }

        public static int ToInt32(this string valor, int padrao)
        {
            if (string.IsNullOrEmpty(valor))
                return padrao;

            int resultado = 0;
            int.TryParse(valor, out resultado);
            return resultado;
        }

        public static int ToInt32(this object valor, int padrao)
        {
            if (valor == null)
                return padrao;

            int resultado = 0;
            int.TryParse(valor.ToString(), out resultado);
            return resultado;
        }

        #endregion ToInt32

        #region ToDecimal

        public static decimal ToDecimal(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return 0;

            decimal resultado = 0;
            decimal.TryParse(valor, out resultado);
            return resultado;
        }

        public static decimal ToDecimal(this object valor)
        {
            if (valor == null)
                return 0;

            decimal resultado = 0;
            decimal.TryParse(valor.ToString(), out resultado);
            return resultado;
        }

        #endregion ToDecimal

        #region DecimalToStringUSA
        public static string DecimalToStringUSA(this decimal decimalNumber)
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo();
            numberFormatInfo.NumberDecimalSeparator = ".";

            return decimalNumber.ToString(numberFormatInfo);
        }
        #endregion

        #region ToGuid

        public static Guid ToGuid(this object valor)
        {
            if (valor == null)
                return Guid.Empty;

            Guid resultado = Guid.Empty;
            Guid.TryParse(valor.ToString(), out resultado);
            return resultado;
        }

        public static Guid ToGuid(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return Guid.Empty;

            Guid resultado = Guid.Empty;
            Guid.TryParse(valor, out resultado);
            return resultado;
        }

        public static Guid? ToGuidOrNull(this string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return null;

            Guid resultado = Guid.Empty;
            if (Guid.TryParse(valor, out resultado))
                return resultado;
            else
                return null;
        }

        public static Guid? ToGuidOrNull(this object valor)
        {
            if (valor == null)
                return null;

            Guid resultado = Guid.Empty;
            if (Guid.TryParse(valor.ToString(), out resultado))
                return resultado;
            else
                return null;
        }

        public static Guid NewIfEmpty(this Guid valor)
        {
            if (valor == Guid.Empty)
            {
                return Guid.NewGuid();
            }
            else
            {
                return valor;
            }
        }

        #endregion ToGuid

        #region Replace Latin Caracters

        public static string ReplaceLatinCaracters(this string Palavra)
        {
            Palavra = Palavra.Replace("&aacute;", "á");
            Palavra = Palavra.Replace("&Aacute;", "Á");
            Palavra = Palavra.Replace("&atilde;", "ã");
            Palavra = Palavra.Replace("&Atilde;", "Ã");
            Palavra = Palavra.Replace("&acirc;", "â");
            Palavra = Palavra.Replace("&Acirc;", "Â");
            Palavra = Palavra.Replace("&agrave;", "à");
            Palavra = Palavra.Replace("&Agrave;", "À");
            Palavra = Palavra.Replace("&eacute;", "é");
            Palavra = Palavra.Replace("&Eacute;", "É");
            Palavra = Palavra.Replace("&ecirc;", "ê");
            Palavra = Palavra.Replace("&Ecirc;", "Ê");
            Palavra = Palavra.Replace("&iacute;", "í");
            Palavra = Palavra.Replace("&Iacute;", "Í");
            Palavra = Palavra.Replace("&oacute;", "ó");
            Palavra = Palavra.Replace("&Oacute;", "Ó");
            Palavra = Palavra.Replace("&otilde;", "õ");
            Palavra = Palavra.Replace("&Otilde;", "Õ");
            Palavra = Palavra.Replace("&ocirc;", "ô");
            Palavra = Palavra.Replace("&Ocirc;", "Ô");
            Palavra = Palavra.Replace("&uacute;", "ú");
            Palavra = Palavra.Replace("&Uacute;", "Ú");
            Palavra = Palavra.Replace("&uuml;", "ü");
            Palavra = Palavra.Replace("&Uuml;", "Ü");
            Palavra = Palavra.Replace("&Ccedil;", "Ç");
            Palavra = Palavra.Replace("&ccedil;", "ç");
            Palavra = Palavra.Replace("&ndash;", "-");
            return Palavra;
        }

        #endregion Replace Latin Caracters

        #region Remove acento

        public static string NormalizaString(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = new string(text.Trim().Normalize(NormalizationForm.FormD).Where(c => c < 128).ToArray());
            text = text.ToLower();

            return text;
        }

        public static bool Contains(this string strA, string strB, bool ignoreCaseCulture)
        {
            if (!ignoreCaseCulture)
                return strA.Contains(strB);

            return strA.RemoveAcento().ToLower().Contains(strB.RemoveAcento().ToLower());
        }

        public static bool EqualsIgnoreCaseCulture(this string strA, string strB)
        {
            if (string.IsNullOrEmpty(strA) && !string.IsNullOrEmpty(strB))
                return false;

            if (!string.IsNullOrEmpty(strA) && string.IsNullOrEmpty(strB))
                return false;

            return strA.RemoveAcento().ToLower().Trim().Equals(strB.RemoveAcento().ToLower().Trim());
        }

        public static string RemoveAcento(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();

            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        #endregion Remove acento

        #region Remove Cadacteres Especiais

        public static string RemoveCaracteresEspeciais(this string inputString)
        {
            inputString = inputString.Normalize(NormalizationForm.FormKD);
            StringBuilder sb = new StringBuilder();
            foreach (char c in inputString)
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);

            string output = new string(sb.ToString().Where(c => !char.IsControl(c)).ToArray());

            return output;
        }

        #endregion Remove Cadacteres Especiais

        #region Numero por extenso

        private static List<Int64> numeroLista;
        private static Int64 num;

        private static readonly String[,] qualificadores = new String[,] {
                {"Centavo", "Centavos"},
                {"", ""},
                {"Mil", "Mil"},
                {"Milhão", "Milhões"},
                {"Bilhão", "Bilhões"},
                {"Trilhão", "Trilhões"},
                {"Quatrilhão", "Quatrilhões"},
                {"Quintilhão", "Quintilhões"},
                {"Sextilhão", "Sextilhões"},
                {"Setilhão", "Setilhões"},
                {"Octilhão","Octilhões"},
                {"Nonilhão","Nonilhões"},
                {"Decilhão","Decilhões"}
        };

        private static readonly String[,] numeros = new String[,] {
                {"Zero", "Hum", "Dois", "Três", "Quatro",
                 "Cinco", "Seis", "Sete", "Oito", "Nove",
                 "Dez","Onze", "Doze", "Treze", "Quatorze",
                 "Quinze", "Dezesseis", "Dezessete", "Dezoito", "Dezenove"},
                {"Vinte", "Trinta", "Quarenta", "Cinquenta", "Sessenta",
                 "Setenta", "Oitenta", "Noventa",null,null,null,null,null,null,null,null,null,null,null,null},
                {"Cem", "Cento","Duzentos", "Trezentos", "Quatrocentos", "Quinhentos", "Seiscentos",
                 "Setecentos", "Oitocentos", "Novecentos",null,null,null,null,null,null,null,null,null,null}
                };

        private static void SetNumero(Decimal dec)
        {
            dec = Decimal.Round(dec, 2);
            dec = dec * 100;
            num = Convert.ToInt64(dec);

            numeroLista = new List<Int64>();

            if (num == 0)
            {
                numeroLista.Add(0);
                numeroLista.Add(0);
            }
            else
            {
                AddRemainder(100);

                while (num != 0)
                {
                    AddRemainder(1000);
                }
            }
        }

        private static void AddRemainder(Int32 divisor)
        {
            Int64 div = num / divisor;
            Int64 mod = num % divisor;

            numeroLista.Add(mod);
            num = div;
        }

        private static bool EhPrimeiroGrupoUm()
        {
            if ((Int32)numeroLista[numeroLista.Count - 1] == 1)
                return true;
            else
                return false;
        }

        private static bool EhGrupoZero(Int32 ps)
        {
            if (ps <= 0 || ps >= numeroLista.Count)
                return true;

            return ((Int32)numeroLista[ps] == 0);
        }

        private static bool EhUnicoGrupo()
        {
            if (numeroLista.Count <= 3)
                return false;

            if (!EhGrupoZero(1) && !EhGrupoZero(2))
                return false;

            bool hasOne = false;

            for (Int32 i = 3; i < numeroLista.Count; i++)
            {
                if ((Int32)numeroLista[i] != 0)
                {
                    if (hasOne)
                        return false;

                    hasOne = true;
                }
            }
            return true;
        }

        private static String NumToString(Int32 numero, Int32 escala)
        {
            Int32 unidade = (numero % 10);
            Int32 dezena = (numero % 100);
            Int32 centena = (numero / 100);
            StringBuilder buf = new StringBuilder();

            if (numero != 0)
            {
                if (centena != 0)
                {
                    if (dezena == 0 && centena == 1)
                    {
                        buf.Append(numeros[2, 0]);
                    }
                    else
                    {
                        buf.Append(numeros[2, centena]);
                    }
                }

                if (buf.Length > 0 && dezena != 0)
                {
                    buf.Append(" e ");
                }

                if (dezena > 19)
                {
                    dezena = dezena / 10;
                    buf.Append(numeros[1, dezena - 2]);

                    if (unidade != 0)
                    {
                        buf.Append(" e ");
                        buf.Append(numeros[0, unidade]);
                    }
                }
                else if (centena == 0 || dezena != 0)
                {
                    buf.Append(numeros[0, dezena]);
                }

                buf.Append(" ");

                if (numero == 1)
                {
                    buf.Append(qualificadores[escala, 0]);
                }
                else
                {
                    buf.Append(qualificadores[escala, 1]);
                }
            }
            return buf.ToString();
        }

        private static string ToString(decimal valor, bool adicionaReis)
        {
            SetNumero(valor);
            StringBuilder buf = new StringBuilder();
            Int32 count;

            for (count = numeroLista.Count - 1; count > 0; count--)
            {
                if (buf.Length > 0 && !EhGrupoZero(count))
                {
                    buf.Append(" e ");
                }
                buf.Append(NumToString((Int32)numeroLista[count], count));
            }

            if (buf.Length > 0)
            {
                while (buf.ToString().EndsWith(" "))
                    buf.Length = buf.Length - 1;

                if (EhUnicoGrupo())
                {
                    buf.Append(" de ");
                }

                if (adicionaReis)
                {
                    if (numeroLista.Count == 2 && ((Int32)numeroLista[1] == 1))
                    {
                        buf.Append(" Real");
                    }
                    else
                    {
                        buf.Append(" Reais");
                    }
                }

                if ((Int32)numeroLista[0] != 0)
                {
                    buf.Append(" e ");
                }
            }

            if ((Int32)numeroLista[0] != 0)
            {
                buf.Append(NumToString((Int32)numeroLista[0], 0));
            }

            return buf.ToString();
        }

        public static string ToExtenso(this decimal valor, bool adicionaReis = true)
        {
            valor = Math.Abs(valor);
            if (valor == 0)
                return string.Empty;

            return ToString(valor, adicionaReis);
        }

        #endregion Numero por extenso

        #region Referencia

        /// <summary>
        /// Recebe uma data e devolve string no formato m/yyyy onde m é o mês por extenso Ex: Janeiro/2011
        /// </summary>
        /// <param name="Data">um valor do tipo Datetime</param>
        /// <returns>mês por extenso/ano</returns>
        public static string Referencia(this DateTime Data, bool ano)
        {
            return string.Format("{0}{1}{2}", meses[Data.Month - 1], (ano ? "/" : string.Empty), (ano ? Data.Year.ToString() : string.Empty));
        }

        public static string Referencia(this DateTime Data, bool ano, bool abreviado)
        {
            return string.Format("{0}{1}{2}", meses[Data.Month - 1].Substring(0, 3), (ano ? "/" : string.Empty), (ano ? Data.Year.ToString() : string.Empty));
        }

        public static string Referencia(this int Mes)
        {
            return string.Format("{0}", meses[Mes - 1]);
        }

        public static string Referencia(this object valor)
        {
            int resultado;
            int.TryParse(valor.ToString(), out resultado);

            return string.Format("{0}", meses[resultado - 1]);
        }

        /// <summary>
        /// devolve string no formato m/yyyy onde m é o mês por extenso Ex: Janeiro/2011
        /// </summary>
        /// <param name="valor">objeto do tipo Datetime</param>
        /// <returns></returns>
        public static string Referencia(this object valor, bool ano)
        {
            DateTime resultado;
            DateTime.TryParse(valor.ToString(), out resultado);
            return string.Format("{0}{1}{2}", meses[resultado.Month - 1], (ano ? "/" : string.Empty), (ano ? resultado.Year.ToString() : string.Empty));
        }

        public static string RemovePontoVirgula(this object valor)
        {
            return valor.ToString().Replace(".", string.Empty).Replace(",", string.Empty);
        }

        public static string RemoveMascaraCPFCNPJ(this object valor)
        {
            return valor.ToString().Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty).Replace("_", string.Empty).RemoveAcento().Replace(" ", string.Empty);
        }

        public static string RemoveMascaraCEP(this object valor)
        {
            return valor.ToString().Replace(".", string.Empty).Replace("-", string.Empty);
        }

        public static string RemoveMascaraTelefone(this object valor)
        {
            return valor.ToString().Replace("(", string.Empty).Replace("-", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty);
        }

        #endregion Referencia

        #region CNPJ - CPF

        public static string CEPFormatado(this string valor)
        {
            const string MASCARA = "##.###-###";

            StringBuilder dado = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = MASCARA.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0;)
            {
                if (MASCARA[--indMascara] == '#')
                    indCampo--;
            }

            StringBuilder saida = new StringBuilder();
            for (; indMascara < MASCARA.Length; indMascara++)
                saida.Append((MASCARA[indMascara] == '#') ? dado[indCampo++] : MASCARA[indMascara]);

            return saida.ToString();
        }

        public static string CEPFormatadoSemPonto(this string valor)
        {
            const string MASCARA = "#####-###";

            StringBuilder dado = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = MASCARA.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0;)
            {
                if (MASCARA[--indMascara] == '#')
                    indCampo--;
            }

            StringBuilder saida = new StringBuilder();
            for (; indMascara < MASCARA.Length; indMascara++)
                saida.Append((MASCARA[indMascara] == '#') ? dado[indCampo++] : MASCARA[indMascara]);

            return saida.ToString();
        }

        public static string CPFFormatado(this string valor)
        {
            const string MASCARA = "###.###.###-##";
            StringBuilder dado = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = MASCARA.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0;)
            {
                if (MASCARA[--indMascara] == '#')
                    indCampo--;
            }

            indMascara = 0;
            StringBuilder saida = new StringBuilder();
            for (; indCampo < dado.Length; indMascara++)
                saida.Append((MASCARA[indMascara] == '#') ? dado[indCampo++] : MASCARA[indMascara]);

            return saida.ToString();
        }

        public static string CNPJFormatado(this string valor)
        {
            const string MASCARA = "##.###.###/####-##";
            StringBuilder dado = new StringBuilder();
            // remove caracteres nao numericos
            foreach (char c in valor)
            {
                if (Char.IsNumber(c))
                    dado.Append(c);
            }

            int indMascara = MASCARA.Length;
            int indCampo = dado.Length;

            for (; indCampo > 0 && indMascara > 0;)
            {
                if (MASCARA[--indMascara] == '#')
                    indCampo--;
            }

            indMascara = 0;

            StringBuilder saida = new StringBuilder();
            for (; indCampo < dado.Length; indMascara++)
                saida.Append((MASCARA[indMascara] == '#') ? dado[indCampo++] : MASCARA[indMascara]);

            return saida.ToString();
        }

        public static bool CPFCNPJEstaVazio(string param)
        {
            if (string.IsNullOrWhiteSpace(param))
                return true;
            var converteu = long.TryParse(param, out long resultado);
            return converteu && resultado == 0;
        }

        #endregion CNPJ - CPF

        #region Operações de arredondamento

        /// <summary>
        /// Arredonda um valor para duas casas decimais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal Arredondar(this decimal value)
        {
            if (value == 0)
                return 0;

            return Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Arredonda um valor para duas casas decimais
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal Arredondar(this decimal value, int casas)
        {
            if (value == 0)
                return 0;

            return Math.Round(value, casas, MidpointRounding.AwayFromZero);
        }

        #endregion Operações de arredondamento com decimais

        #region ToUltimoDiaMes / ToPrimeiraDiaMes

        public static DateTime ToUltimoDiaMes(this DateTime valor)
        {
            //ultimo dia do mes
            return new DateTime(valor.Year, valor.Month, DateTime.DaysInMonth(valor.Year, valor.Month));
        }

        /// <summary>
        /// Metodo criado para adicionar um dia determinado em uma data,
        /// faz a validação antes de criar a data ,se está é uma data válida
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>

        public static DateTime ToPrimeiraDiaMes(this DateTime valor)
        {
            //primeiro dia do mes
            return new DateTime(valor.Year, valor.Month, 1);
        }

        #endregion ToUltimoDiaMes / ToPrimeiraDiaMes

        public static bool IsUltimoDiaMes(this DateTime valor)
        {
            if (valor.Date == ToUltimoDiaMes(valor).Date)
                return true;
            else
                return false;
        }

        #region SplitUpperCaseToString

        public static string SplitUpperCaseToString(this string source)
        {
            if (source == null)
            {
                return null;
            }
            return string.Join(" ", source.SplitUpperCase());
        }

        public static string[] SplitUpperCase(this string source)
        {
            if (source == null)
                return new string[] { }; //Return empty array.

            if (source.Length == 0)
                return new string[] { "" };

            StringCollection words = new StringCollection();
            int wordStartIndex = 0;

            char[] letters = source.ToCharArray();
            char previousChar = char.MinValue;
            // Skip the first letter. we don't care what case it is.
            for (int i = 1; i < letters.Length; i++)
            {
                if (char.IsUpper(letters[i]) && !char.IsWhiteSpace(previousChar))
                {
                    //Grab everything before the current index.
                    words.Add(new String(letters, wordStartIndex, i - wordStartIndex));
                    wordStartIndex = i;
                }
                previousChar = letters[i];
            }
            //We need to have the last word.
            words.Add(new String(letters, wordStartIndex, letters.Length - wordStartIndex));

            //Copy to a string array.
            string[] wordArray = new string[words.Count];
            words.CopyTo(wordArray, 0);
            return wordArray;
        }

        #endregion SplitUpperCaseToString

        #region Números Romanos

        public static string ToRomanNumber(this object valor)
        {
            int integerNumber;
            int.TryParse(valor.ToString(), out integerNumber);
            if (integerNumber < 1)
                return String.Empty;
            if (integerNumber > 3999)
                throw new ArgumentException("integerNumber. Should be less than 3999");

            const string romanSymbols = "IVXLCDM";
            var orderOfNumber = (int)Math.Truncate(Math.Log10(integerNumber));

            var currentNumber = integerNumber;
            var sbRomanNumber = new StringBuilder();

            for (var i = orderOfNumber; i >= 0; i--)
            {
                int j = i * 2;
                int devisor = GetCurrentDevisor(i);
                int valueOfCurrentPower = (int)Math.Truncate((double)(currentNumber / devisor));

                switch (valueOfCurrentPower)
                {
                    case 1:
                        sbRomanNumber.Append(romanSymbols[j]);
                        break;

                    case 2:
                        sbRomanNumber.Append(romanSymbols[j], 2);
                        break;

                    case 3:
                        sbRomanNumber.Append(romanSymbols[j], 3);
                        break;

                    case 4:
                        sbRomanNumber.Append(romanSymbols[j]);
                        sbRomanNumber.Append(romanSymbols[j + 1]);
                        break;

                    case 5:
                        sbRomanNumber.Append(romanSymbols[j + 1]);
                        break;

                    case 6:
                        sbRomanNumber.Append(romanSymbols[j + 1]);
                        sbRomanNumber.Append(romanSymbols[j]);
                        break;

                    case 7:
                        sbRomanNumber.Append(romanSymbols[j + 1]);
                        sbRomanNumber.Append(romanSymbols[j], 2);
                        break;

                    case 8:
                        sbRomanNumber.Append(romanSymbols[j + 1]);
                        sbRomanNumber.Append(romanSymbols[j], 3);
                        break;

                    case 9:
                        sbRomanNumber.Append(romanSymbols[j]);
                        sbRomanNumber.Append(romanSymbols[j + 2]);
                        break;
                }
                currentNumber -= valueOfCurrentPower * devisor;
            }
            return sbRomanNumber.ToString();
        }

        public static int FromRomanNumber(this string romanNumber)
        {
            if (romanNumber == null || string.IsNullOrEmpty(romanNumber))
                throw new ArgumentNullException("romanNumber");

            int resultInteger = 0;

            romanNumber = romanNumber.ToUpperInvariant();

            for (int i = 0; i < romanNumber.Length; i++)
            {
                switch (romanNumber[i])
                {
                    case 'I':
                        if (i < romanNumber.Length - 1
                            && romanNumber[i + 1] != 'I')
                            resultInteger--;
                        else resultInteger++;
                        break;

                    case 'V':
                        resultInteger += 5;
                        break;

                    case 'X':
                        if (i < romanNumber.Length - 1
                            && (romanNumber[i + 1] == 'L'
                                || romanNumber[i + 1] == 'C'))
                            resultInteger -= 10;
                        else resultInteger += 10;
                        break;

                    case 'L':
                        resultInteger += 50;
                        break;

                    case 'C':
                        if (i < romanNumber.Length - 1
                            && (romanNumber[i + 1] == 'D'
                                || romanNumber[i + 1] == 'M'))
                            resultInteger -= 100;
                        else
                            resultInteger += 100;
                        break;

                    case 'D':
                        resultInteger += 500;
                        break;

                    case 'M':
                        resultInteger += 1000;
                        break;

                    default:
                        throw new ArgumentException("Wrong Roman number format.");
                }
            }
            return resultInteger;
        }

        private static int GetCurrentDevisor(int i)
        {
            return (int)Math.Pow(10, i);
        }

        #endregion Números Romanos

        #region Tamanho de arquivos - Bytes, KBytes e etc

        private static string getBytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB

            if (byteCount == 0)
                return "0" + suf[0];

            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public static string BytesToString(this int byteCount)
        {
            return getBytesToString(byteCount);
        }

        public static string BytesToString(this long byteCount)
        {
            return getBytesToString(byteCount);
        }

        #endregion Tamanho de arquivos - Bytes, KBytes e etc

        #region Numeros Formatos Casas Numericos

        /// <summary>
        /// Retorna o valor formatado ex:
        /// 10 => 10,00
        /// 100 => 100,00
        /// 1000 =>1.000,00
        /// 10000 =>10.000,00
        ///  1000000 =>1.000.000,00
        /// </summary>
        /// <param name="valor">valor no formato decimal </param>
        /// <returns>retorna o valor formatado</returns>
        public static string FormataCasasMilhar(this decimal valor)
        {
            return string.Format("{0:0,0.00}", valor);
        }

        #endregion Numeros Formatos Casas Numericos

        #region Enum Parse

        public static T ToEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        #endregion Enum Parse

        public static int Idade(this DateTime nascimento)
        {
            return Idade(nascimento, DateTime.Now);
        }

        public static int Idade(this DateTime nascimento, DateTime dtReferencia)
        {
            int idade = dtReferencia.Year - nascimento.Year;

            if (dtReferencia < nascimento.AddYears(idade))
                idade--;

            return idade;
        }

        public static string GetValorTruncado(this decimal valor)
        {
            decimal valorTruncado = Math.Truncate(valor);
            string retorno = (valor - valorTruncado == 0) ? valorTruncado.ToString("N0") : valor.ToString("N2");
            return retorno;
        }

        #region Sinonimos

        private static List<List<string>> listaSinonimos = null;

        public static List<string> GetSinonimo(this string valor)
        {
            List<string> retorno = new List<string>();
            retorno.Add(valor);

            if (listaSinonimos == null)
                PopulaTabela();

            valor = valor.ToLower();

            foreach (var termos in listaSinonimos)
            {
                foreach (var sinonimo in termos)
                {
                    if (sinonimo.ToLower() == valor)
                    {
                        retorno.AddRange(termos.ToArray());
                        return retorno;
                    }
                }
            }

            return retorno.Distinct().ToList();
        }

        private static void PopulaTabela()
        {
            listaSinonimos = new List<List<string>>();

            for (int i = 1; i < 1000; i++)
            {
                listaSinonimos.Add(new List<string>()
                {
                    decimal.Parse(i.ToString()).ToExtenso(false),
                    i.ToString(),
                    i.ToRomanNumber()
                });
            }
        }

        #endregion Sinonimos

        public static bool MoveToFront<T>(this List<T> list, Predicate<T> match)
        {
            int idx = list.FindIndex(match);

            if (idx != -1)
            {
                if (idx != 0) // move only if not already in front
                {
                    T value = list[idx]; // save matching value
                    list.RemoveAt(idx); // remove it from original location
                    list.Insert(0, value); // insert in front
                }
                return true;
            }

            return false; // matching value not found
        }

        #region Transforma uma string em um nome válido de arquivo

        public static string ToValidFileName(this string name)
        {
            return ToValidFileName(name, "_");
        }

        public static string ToValidFileName(this string name, string replacement)
        {
            string invalidChars = System.Text.RegularExpressions.Regex.Escape(new string(System.IO.Path.GetInvalidFileNameChars()));
            string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", invalidChars);

            return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, replacement);
        }

        #endregion Transforma uma string em um nome válido de arquivo

        #region Recupera apenas os números de uma string

        /// <summary>
        /// Recupera apenas os números de uma string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string GetNumeros(this string texto)
        {
            string resultString = null;
            try
            {
                Regex regexObj = new Regex(@"[^\d]");
                resultString = regexObj.Replace(texto, "");
            }
            catch (ArgumentException)
            {
                resultString = string.Empty;
            }

            return resultString;
        }

        #endregion Recupera apenas os números de uma string

        #region Recupera apenas as letras de uma string

        /// <summary>
        /// Recupera apenas as letras de uma string
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static string GetLetras(this string texto)
        {
            string resultString = null;
            try
            {
                Regex regexObj = new Regex(@"[^ a-z A-Z]");
                resultString = regexObj.Replace(texto, "");
            }
            catch (ArgumentException)
            {
                resultString = string.Empty;
            }

            return resultString;
        }

        #endregion Recupera apenas as letras de uma string

        public static DateTime ToDateTime(this string str)
        {
            return ToDateTime(str, "YYYY-MM-dd HH:mm:ss");
        }

        public static DateTime ToDateTime(this string str, string format)
        {
            DateTime dt;

            DateTime.TryParseExact(str, format, null, DateTimeStyles.None, out dt);

            return dt;
        }

        public static string ToSqlDateTimeString(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// Retorna quantidade de caracteres informados a partir da esquerda
        /// Exemplo:     "Teste".Left(2)  = Te
        /// </summary>
        /// <param name="param">cadeia de caracteres</param>
        /// <param name="length">quantidade de caracteres</param>
        /// <returns>cadeia de caracteres a partir da esquerda</returns>
        public static string Left(this string param, int length)
        {
            string result = param.Substring(0, param.Length > length ? length : param.Length);
            return result.TrimEnd();
        }

        /// <summary>
        /// Retorna quantidade de caracteres informados a partir da direita
        /// Exemplo:     "Teste".Right(2)  = te
        /// </summary>
        /// <param name="param">cadeia de caracteres</param>
        /// <param name="length">quantidade de caracteres</param>
        /// <returns>cadeia de caracteres a partir da direita</returns>

        public static string Right(this string param, int length)
        {
            string result = param.Substring(length > param.Length ? 0 : param.Length - length, length > param.Length ? param.Length : length);
            return result.TrimEnd();
        }

        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            if (originalString == null)
                return null;
            if (oldValue == null)
                throw new ArgumentNullException("oldValue");
            if (oldValue == string.Empty)
                return originalString;
            if (newValue == null)
                throw new ArgumentNullException("newValue");

            const int indexNotFound = -1;
            int startIndex = 0, index = 0;
            while ((index = originalString.IndexOf(oldValue, startIndex, comparisonType)) != indexNotFound)
            {
                originalString = originalString.Substring(0, index) + newValue + originalString.Substring(index + oldValue.Length);
                startIndex = index + newValue.Length;
            }

            return originalString;
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string valor)
        {
            return string.IsNullOrEmpty(valor) || string.IsNullOrWhiteSpace(valor);
        }

        public static bool ToBoolean(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;

            var palavrasChaveParaVerdadeiro = new List<string> { "sim", "s", "1", "true", "verdadeiro", "verdade", "yes", "y" };

            if (palavrasChaveParaVerdadeiro.Contains(value.ToLowerInvariant()))
                return true;

            return false;
        }

        /// <summary>
        /// Retorna, de uma string, a primeira letra de cada palavra em máiúscula e as outras minúsculas, exceto preposições incluídas na lista
        /// Exemplo:     "SAO JOAO DE MIRITI ".Captalize  = Sao Joao de Miriti
        /// </summary>
        /// <param name="texto">cadeia de caracteres</param>
        /// <returns>texto formatado</returns>
        public static string Capitalize(this string texto)
        {
            var exceto = new List<string>() { "e", "da", "de", "do", "das", "dos" };
            var textoFormatado = new List<string>();

            foreach (var palavra in texto.Split(' '))
            {
                textoFormatado.Add((!exceto.Contains(palavra.ToLower()))
                    ? System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(palavra.ToLower())
                    : palavra.ToLower());
            }

            return textoFormatado.Aggregate((a, b) => a + ' ' + b);
        }

        public static string GetIniciais(this string texto)
        {
            Regex initials = new Regex(@"(\b[a-zA-Z])[a-zA-Z]* ?");
            string init = initials.Replace(texto, "$1");

            return init;
        }

        #region Verifica se possui valor diferente de nulo
        public static bool PossuiValor(this Guid valor)
        {
            return (valor != Guid.Empty);
        }

        public static bool PossuiValor(this Guid? valor)
        {
            return (valor != null && valor != Guid.Empty);
        }

        public static bool PossuiValor(this string valor)
        {
            return (!string.IsNullOrEmpty(valor) && !string.IsNullOrWhiteSpace(valor));
        }

        public static bool PossuiValor(this DateTime valor)
        {
            return (valor != DateTime.MinValue);
        }

        public static bool PossuiValor(this DateTime? valor)
        {
            return (valor != null && valor != DateTime.MinValue);
        }

        public static bool PossuiValor(this decimal valor)
        {
            return (valor > 0);
        }

        public static bool PossuiValor(this decimal? valor)
        {
            return (valor != null && valor > 0);
        }

        public static bool PossuiValor(this int valor)
        {
            return (valor > 0);
        }

        public static bool PossuiValor(this int? valor)
        {
            return (valor != null && valor > 0);
        }

        public static bool PossuiValor(this object valor)
        {
            return valor != null;
        }
        #endregion
    }
}