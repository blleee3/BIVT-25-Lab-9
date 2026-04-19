namespace Lab9.White
{
    public class Task4 : White
    {
        private int _output;

        public Task4(string text) : base(text)
        {
        }

        public int Output
        {
            get
            {
                return _output;
            }
        }

        public override void Review()
        {
            _output = CalculateSumOfDigits(Input);
        }

        private int CalculateSumOfDigits(string text)
        {
            int s = 0;
            foreach (char ch in text)
                if (char.IsDigit(ch)) //является ли символ цифрой (от '0' до '9')
                    s += ch - '0';
            return s;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
