using System.Collections.Generic;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Security.Policy;

namespace MemoryGame_UPGRADED
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Image> ImgObj = new List<Image>();
        List<Button> BtnObj = new List<Button>();
        List<string> imageNames = new List<string>()// יצירת אטוסף של שמות התמונות   שנרצה להציג 
        //{"cactus_wilting_md_wht.gif","m3_login_he.png","brownbear2_lg_clr.gof","anglkiss.gif"};
        {"cactus_wilting_md_wht.gif","Car_1.png","brownbear.gif","anglkiss.gif"};
        Random rnd = new Random();

        int mone = 0;// מונה למספר זוגות שנחשפו  
        int count = 4;//מספר זוגות הקלפים במשחק 
        int move = 0;
        Button btn1, btn2;

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement ctrl in this.myGrid.Children)
            {
                if (ctrl is Button) this.BtnObj.Add(ctrl as Button);
                else
                if (ctrl is Image) this.ImgObj.Add(ctrl as Image);
            }

            while (imageNames.Count > 0)
            {
                int img = rnd.Next(imageNames.Count);// נבחר מספר תמונות רנדומלי 
                //נייצור, בצורה דינרמית או כתובת (בדיסק) של התמונה בתוך הפרוייקט
                var uri = new Uri("pack://application:,,,/Images/" + imageNames[img]);
                var bitmap = new BitmapImage(uri); // "נייור "תמונה

                for (int i = 0; i < 2; i++) // נבצע זאת פעמיים , כי יש לנו זוג כפתורים  ותמונות 
                {
                    int inx = findFreeBtn();//  ללא תמונה , (ללא תג), "נחפש מיקום של "כפתור פנוי ,"
                    ImgObj[inx].Source = bitmap;// נשים בו את התמונה 
                    BtnObj[inx].Tag = imageNames[img];// ונשים ב - תג שלו את שם התמונה , לזיהוי  
                    //הייתה פה שגיא עקב COPY PASTE 

                }
                imageNames.RemoveAt(img);

            }
        }
        private int findFreeBtn()
        {
            int inx = -1;
            int x;

            while (inx < 0)
            {
                x = rnd.Next(BtnObj.Count);
                if (BtnObj[x].Tag == null)
                    inx = x;

            }
            return inx;
        }
        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            move++;
            btn.Visibility = Visibility.Collapsed;  // לחשוף את הקלף שנלחץ 
            if (btn1 == null) //האם זה הקלף הראשון 
            {
                btn1 = btn; //לשמור את הקלף הראשון  
            }
            else
            {
                if (btn2 == null) // האם זהו הכלף השני  
                {
                    btn2 = btn; // לשמור את הקלף השני                  

                    if (count == mone + 1)// האם המשחק הסתיים ??
                    {
                        MessageBox.Show("  כל הכבוד ,ניצחת ב " + move + "מהלכים !!!");
                    }
                }

                else
                {
                    //if (int.Parse(btn1.Tag.ToString()) == int.Parse(btn2.Tag.ToString()))
                    //if (btn1.Tag.ToString() == btn2.Tag.ToString())
                    if (btn1.Tag.Equals(btn2.Tag)) //... אם נחשפו זוג קלפים 
                    {
                        mone = mone + 1;
                        // זוג שנחשף ישאר חשוף  
                    }
                    else  // הסתר את הזוג הקלפים שנחשפו
                    {

                        btn1.Visibility = Visibility.Visible;
                        btn2.Visibility = Visibility.Visible;
                    }
                    btn1 = btn; //שמור את הקלף שנלחץ כקלף ראשון  
                    btn2 = null; // אין לנו עדיין קלף שני 


                }
            }

        }
    }
}