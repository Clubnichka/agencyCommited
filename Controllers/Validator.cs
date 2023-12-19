namespace agency.Controllers
{
    public static class Validator
    {
        public static bool EmailValid(String password)
        {
            Console.WriteLine("Validation Started");
            bool valid = true;
            bool isa = false;
            bool isd=false;
            int a=0;
            int d=0;
            if (password == null)
            {
                Console.WriteLine("NULL");
                valid = false;
                return false;
            }
            if (password.Length<5)
            {
                Console.WriteLine("SHORT");
                valid = false;
                return false;
            }
            foreach (char i in password)
            {
                if (i.Equals('@'))
                {
                    isa= true;
                    a = password.IndexOf(i);
                    Console.WriteLine(a);
                    break;
                }
            }
            if (!isa)
            {
                Console.WriteLine("NO @");
                valid = false;
                return false;
            }
            foreach (char i in password)
            {
                if (i.Equals('.'))
                {
                    isd = true;
                    d = password.IndexOf(i);
                    Console.WriteLine(d);
                    break;
                }
            }
            if (!isd)
            {
                valid = false;
                Console.WriteLine("NO DOT");
                return false;
            }
            if (a == 0|| a == password.Length-1){
                valid = false;
                Console.WriteLine("Incorrect @");
                return false;

            }
            if (d == 0 || d== password.Length - 1)
            {
                valid = false;
                Console.WriteLine("Incorrect DOT");
                return false;
            }
            if (a-d>=-2) {
                valid = false;
                Console.WriteLine("Incorrect DOT before @");
                return false;
            }
            return valid;
        }
        public static bool PassValid(String  password)
        {
            if (password.Length > 4)
            {
                return true;
            }
            else
            {
                    return false;
            }
        }
    }
}
