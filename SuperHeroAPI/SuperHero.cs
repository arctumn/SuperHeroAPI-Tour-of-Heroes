namespace SuperHeroAPI
{
    public class SuperHero
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;

        public void Update(SuperHero hero) 
        {
            ID = hero.ID;
            Name = hero.Name;
            FirstName = hero.FirstName;
            LastName = hero.LastName;
            Place = hero.Place;
        }
    }
}
