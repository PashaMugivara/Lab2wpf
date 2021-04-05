using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;
using System;
using System.Net;
using System.IO;

namespace Lab2wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Menace> menaces = new List<Menace>();
        public static List<Menace> newMenaces = new List<Menace>();
        
        string link = @"https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public MainWindow()
        {
            MessageBoxResult result = new MessageBoxResult();
            if (!File.Exists("thrlist.xlsx"))
            {
                result = MessageBox.Show("Файла с локальной базой не существует. Хотите загрузить файл из интернета?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
                }
                else if (result == MessageBoxResult.No) { MessageBox.Show("Ну тогда тебе тут делать нечего!", "Пока, пока!"); Close(); }
            }
            if (result != MessageBoxResult.No)
            {
                InitializeComponent();
                menaces = EnumerateMenaces("thrlist.xlsx").ToList();
                List<int> RecordsToShow = new List<int>() { 15, 30, 50 };
                NumberOfRecords.ItemsSource = RecordsToShow;
                NumberOfRecords.SelectedItem = 15;
            }
        }
        public void MyHide()
        {
            ListMeance.Visibility = Visibility.Collapsed;
            ListUpdate.Visibility = Visibility.Collapsed;
            Button1.Visibility = Visibility.Collapsed;
            Button2.Visibility = Visibility.Collapsed;
            NumberOfRecords.Visibility = Visibility.Collapsed;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MyHide();
            ListUpdate.Visibility = Visibility.Visible;
            WebClient webClient = new WebClient();
            webClient.DownloadFile(new Uri(link), "thrlist.xlsx");
            List<Changes> changes = new List<Changes>();
            Dictionary<int, int> a = new Dictionary<int, int>();
            newMenaces = EnumerateMenaces("thrlist.xlsx").ToList();
            foreach (var ir in menaces)
            {
                if(!ir.ContainId(newMenaces)) changes.Add(new Changes(ir.Id, "Идентификатор УБИ", ir.Id.ToString(), ""));
                foreach (var ir1 in newMenaces)
                {
                    if (!ir1.ContainId(menaces) && !a.ContainsKey(ir1.Id)) { changes.Add(new Changes(ir1.Id, "Идентификатор УБИ", "", ir1.Id.ToString())); a.Add(ir1.Id, 1); }
                    if (ir.Id == ir1.Id) 
                        {
                        if (ir != ir1)
                        {
                            if (ir.Name != ir1.Name) changes.Add(new Changes(ir.Id, "Наименование УБИ", ir.Name, ir1.Name));
                            if (ir.Description != ir1.Description) changes.Add(new Changes(ir.Id, "Описание", ir.Description, ir1.Description));
                            if (ir.Source != ir1.Source) changes.Add(new Changes(ir.Id, "Источник угрозы (характеристика и потенциал нарушителя)", ir.Source, ir1.Source));
                            if (ir.ObjectOfInfluence != ir1.ObjectOfInfluence) changes.Add(new Changes(ir.Id, "Объект воздействия", ir.ObjectOfInfluence, ir1.ObjectOfInfluence));
                            if (ir.PrivacyViolation != ir1.PrivacyViolation) changes.Add(new Changes(ir.Id, "Нарушение конфиденциальности", ir.PrivacyViolation.ToString(), ir1.PrivacyViolation.ToString()));
                            if (ir.IntegrityViolation != ir1.IntegrityViolation) changes.Add(new Changes(ir.Id, "Нарушение целостности", ir.IntegrityViolation.ToString(), ir1.IntegrityViolation.ToString()));
                            if (ir.AvailabilityViolation != ir1.AvailabilityViolation) changes.Add(new Changes(ir.Id, "Нарушение доступности", ir.AvailabilityViolation.ToString(), ir1.AvailabilityViolation.ToString()));
                            if (ir.ActivationDate != ir1.ActivationDate) changes.Add(new Changes(ir.Id, "Дата включения угрозы в БнД УБИ", ir.ActivationDate.ToString("dd.MM.yyyy"), ir1.ActivationDate.ToString("dd.MM.yyyy")));
                            if (ir.DateOfChange != ir1.DateOfChange) changes.Add(new Changes(ir.Id, "Дата последнего изменения данных", ir.DateOfChange.ToString("dd.MM.yyyy"), ir1.DateOfChange.ToString("dd.MM.yyyy")));
                        }
                    }
                }
            }
            if (changes.Count == 0) MessageBox.Show("Изменений не произошло", "Ошибка");
            else 
            { 
                MessageBox.Show($"Найдено {changes.Count} изменений", "Успешно");
                ListUpdate.ItemsSource = changes;
                menaces = newMenaces;
            }
            Pagging.ThisIndex = 1;
        }
        static IEnumerable<Menace> EnumerateMenaces(string xlsxpath)
        {
            var workbook = new XLWorkbook(xlsxpath);
            var worksheet = workbook.Worksheet(1);
            for (int row = 3; row <= worksheet.LastRowUsed().RowNumber(); ++row)
            {
                // По каждой строке формируем объект
                var metric = new Menace
                {
                    Id = worksheet.Cell(row, 1).GetValue<int>(),
                    Name = worksheet.Cell(row, 2).GetValue<string>(),
                    Description = worksheet.Cell(row, 3).GetValue<string>(),
                    Source = worksheet.Cell(row, 4).GetValue<string>(),
                    ObjectOfInfluence = worksheet.Cell(row, 5).GetValue<string>(),
                    PrivacyViolation = worksheet.Cell(row, 6).GetValue<bool>(),
                    IntegrityViolation = worksheet.Cell(row, 7).GetValue<bool>(),
                    AvailabilityViolation = worksheet.Cell(row, 8).GetValue<bool>(),
                    ActivationDate = worksheet.Cell(row, 9).GetValue<DateTime>(),
                    DateOfChange = worksheet.Cell(row, 10).GetValue<DateTime>()

                };
                yield return metric;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MyHide();
            ListMeance.Visibility = Visibility.Visible;
            Button1.Visibility = Visibility.Visible;
            Button2.Visibility = Visibility.Visible;
            NumberOfRecords.Visibility = Visibility.Visible;
            List<Menace> brief = new List<Menace>();
            for (int i = Pagging.ThisIndex; i <Pagging.ThisIndex+Pagging.Count; i++) brief.Add(menaces[i-1]);
            ListMeance.ItemsSource = brief;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pagging.Count = (int)NumberOfRecords.SelectedItem;
            Pagging.ThisIndex = 1;
            Button_Click_1(this, new RoutedEventArgs());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (Pagging.ThisIndex > Pagging.Count){ Pagging.ThisIndex -= Pagging.Count; }
            Button_Click_1(this, new RoutedEventArgs());
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (Pagging.ThisIndex + 2 * Pagging.Count < menaces.Count) { Pagging.ThisIndex += Pagging.Count; }
            else if (Pagging.ThisIndex + Pagging.Count < menaces.Count) { Pagging.ThisIndex += Pagging.Count; Pagging.Count = menaces.Count - Pagging.ThisIndex+1; }
            if (Pagging.ThisIndex + Pagging.Count <= menaces.Count+1) Button_Click_1(this, new RoutedEventArgs());
            Pagging.Count = (int)NumberOfRecords.SelectedItem;
        }



        private void grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Menace path = ListMeance.SelectedItem as Menace;
            if (path == null) return;
            MessageBox.Show($"ID: {path.Id}\n\n" +
                $"Наименование УБИ: {path.Name}\n\n" +
                $"Описание: {path.Description}\n\n" +
                $"Источник угрозы (характеристика и потенциал нарушителя): {path.Source}\n\n" +
                $"Объект воздействия: {path.ObjectOfInfluence}\n\n" +
                $"Нарушение конфиденциальности: {path.PrivacyViolation}\n\n" +
                $"Нарушение целостности: {path.IntegrityViolation}\n\n" +
                $"Нарушение доступности: {path.AvailabilityViolation}\n\n" +
                $"Дата включения угрозы в БнД УБИ: {path.ActivationDate:dd.MM.yyyy}\n\n" +
                $"Дата последнего изменения данных: {path.DateOfChange:dd.MM.yyyy}");
        }

        private void ListUpdate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }

}
