namespace SMK
{
    class user
    {
        public string Name { get; set; }
        public string Password { get; set; }

        private string message;

        public string user_Email { get; set; }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }
}