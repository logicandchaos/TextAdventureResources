namespace TextAdventureLibrary
{
    public class Person : Noun
    {
        public Die Die { get; private set; }//Make attribute, make required attribute

        public Person() { }
    }
}
