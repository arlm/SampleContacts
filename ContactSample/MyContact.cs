namespace ContactSample
{
    public class MyContact
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public long Id { get; set; }

        public MyContact (string name, string lastName)
        {
            this.Name = name;
            this.LastName = lastName;
        }
    }
}
