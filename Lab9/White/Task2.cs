namespace Lab9.White;
public class Task2 : White
{
    private int[,] _output;

    public Task2(string text) : base(text) { }

    public override void Review()
    {
        _output = sMatrix(Input);
    }

    public int[,] Output => _output ?? new int[0, 2];

    public static int[,] sMatrix(string text)
    {
        var aaWord = new System.Text.StringBuilder(); //для построения слова
        bool WordS = false; //начинается ли слово после цифр 
        bool WordL = false; //был ли последний символ цифрой

        //подсчёт количества слов которые не начинаются после цифр
        int slovass = 0;
        foreach (char c in text)
        {
            if (char.IsLetter(c) || c == '-' || c == '\'')
            {
                if (aaWord.Length == 0)
                    WordS = WordL; //запоминаем слово началось после цифры
                aaWord.Append(c);
                WordL = false;
            }
            else
            {
                //символ не является частью слова => слово закончилось
                if (aaWord.Length > 0)
                {
                    //учитываем слово если оно не началось сразу после цифры
                    if (!WordS)
                        slovass++;
                    aaWord.Clear();
                }
                WordL = char.IsDigit(c); //запоминаем текущий символ это цифра
            }
        }
        // Проверяем последнее слово в строке
        if (aaWord.Length > 0 && !WordS)
            slovass++;

        // Второй проход: заполняем массив слов
        string[] words = new string[slovass];
        aaWord.Clear();
        WordS = false;
        WordL = false;
        int wi = 0; // Индекс для записи слов

        foreach (char c in text)
        {
            if (char.IsLetter(c) || c == '-' || c == '\'')
            {
                if (aaWord.Length == 0)
                    WordS = WordL;
                aaWord.Append(c);
                WordL = false;
            }
            else
            {
                if (aaWord.Length > 0)
                {
                    if (!WordS)
                        words[wi++] = aaWord.ToString();
                    aaWord.Clear();
                }
                WordL = char.IsDigit(c);
            }
        }
        if (aaWord.Length > 0 && !WordS)
            words[wi] = aaWord.ToString();

        char[] vowels = {
            'а','е','ё','и','о','у','ы','э','ю','я',
            'А','Е','Ё','И','О','У','Ы','Э','Ю','Я',
            'a','e','i','o','u','y','A','E','I','O','U','Y'
        };

        //массив для хранения количества слогов в каждом слове
        int[] ssWord = new int[words.Length];
        int mx = 0; //максимальное количество слогов среди слов

        //подсчёт слогов для каждого слова
        for (int i = 0; i < words.Length; i++)
        {
            int s = 0;
            //считаем количество гласных в слове
            foreach (char c in words[i])
                if (Array.IndexOf(vowels, c) >= 0) s++;
            //если гласных нет считаем слово односложным
            if (s == 0) s = 1;
            ssWord[i] = s;
            if (s > mx) mx = s;
        }

        int[,] matrix = new int[mx, 2];

        //подсчёт количества слов для каждого числа слогов
        for (int i = 0; i < words.Length; i++)
        {
            int s = ssWord[i];
            if (s > 0)
            {
                matrix[s - 1, 0] = s;        //сохраняем число слогов
                matrix[s - 1, 1]++;          //увеличиваем счётчик слов с таким числом слогов
            }
        }

        //определяем сколько строк в матрице будут непустыми
        int x = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
            if (matrix[i, 1] > 0) x++;

        //создаём матрицу без пустых строк и заполняем её
        int[,] result = new int[x, 2];
        int resultx = 0;
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            if (matrix[i, 1] > 0)
            {
                result[resultx, 0] = matrix[i, 0]; //количество слогов
                result[resultx, 1] = matrix[i, 1]; //количество слов
                resultx++;
            }
        }

        //матрица автоматически отсортирована по возрастанию числа слогов
        //так как мы проходили от 1 до mx
        return result;
    }

    public override string ToString()
    {
        var matrix = Output;
        if (matrix == null)
            return string.Empty;

        var result = new System.Text.StringBuilder();

        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            result.Append(matrix[i, 0]);  //количество слогов
            result.Append(':');
            result.Append(matrix[i, 1]);  //количество слов
            result.AppendLine();          //переход на новую строку
        }

        //удаляем последний символ перевода строки (условие)
        return result.ToString().TrimEnd();
    }
}