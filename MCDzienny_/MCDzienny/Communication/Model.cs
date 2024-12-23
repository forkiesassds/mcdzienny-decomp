namespace MCDzienny.Communication
{
    public class Model
    {
        public static readonly Model Zombie = new Model("zombie");

        readonly string name;

        Model(string name)
        {
            this.name = name;
        }

        public static implicit operator string(Model model)
        {
            return model.name;
        }
    }
}