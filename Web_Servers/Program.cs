
namespace Web_Servers
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MyapplicationContext());
        }
    }
    class MyapplicationContext : ApplicationContext
    {
        private void onFormClosed(object sender, EventArgs e)
        {
            if (Application.OpenForms.Count == 0)
            {
                ExitThread();
            }
        }
        public MyapplicationContext()
        {
            Form1 f1 = new Form1();
            f1.auto_run();
            /*f1.auto_run(11);
            for (int i = 1; i <= 10; i++)
            {
                new Form1().auto_run(i);
            }*/
            var forms = new List<Form>(){
            f1
        };
            foreach (var form in forms)
            {
                form.FormClosed += onFormClosed;
            }
            foreach (var form in forms)
            {
                form.Show();
            }
        }
    }
}