namespace Lab2wpf
{
    public partial class MainWindow
    {
        public class Changes
        {
            public int Id { get; set; }
            public string Pole { get; set; }
            public string Was { get; set; }
            public string Bycame { get; set; }
            public Changes(int id, string a, string b, string c)
            {
                Id = id;
                Pole = a;
                Was = b;
                Bycame = c;
            }
        }
    }

}
