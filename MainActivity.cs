using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;

namespace nrprime
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        private TextView calculatorText;
        private TextView resultText;

        private string[] number = new string[1];

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            calculatorText = FindViewById<TextView>(Resource.Id.textViewNumar);
            resultText = FindViewById<TextView>(Resource.Id.textViewResult);
        }

        [Java.Interop.Export("ButtonClick")]
        public void ButtonClick(View v)
        {
            Button button = (Button)v;
            if ("0123456789".Contains(button.Text))
                AddDigit(button.Text);
            else if ("PRIM" == button.Text)
                Calculate();
            else
                Reset();
        }

        private void AddDigit(string value)
        {
            number[0] += value;

            UpdateCalculatorText();
        }

        private void Calculate(string newOperator = null)
        {
            int prim = int.Parse(number[0]);

            if (IsPrime(prim))
            {
                //ChangeBGClrToGreen
                resultText.Text = "Numarul este prim!";
            }
            else
            {
                //ChangeBGClrToRed
                resultText.Text = "Numarul nu este prim!";
            }

            UpdateCalculatorText();
        }

        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            

            for (int i = 3; i <= number/2; i += 1)
                if (number % i == 0)
                    return false;

            return true;
        }

        private void Reset()
        {
            number[0] = null;
            UpdateCalculatorText();
            //ChangeBGClrToGray
            resultText.Text = "";
        }

        private void UpdateCalculatorText() => calculatorText.Text = $"{number[0]}";
    }
}