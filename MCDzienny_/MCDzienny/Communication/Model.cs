namespace MCDzienny.Communication
{
    // Token: 0x0200008F RID: 143
    public class Model
    {
        // Token: 0x040001DF RID: 479
        public static readonly Model Zombie = new Model("zombie");

        // Token: 0x040001E0 RID: 480
        private readonly string name;

        // Token: 0x060003D1 RID: 977 RVA: 0x00014304 File Offset: 0x00012504
        private Model(string name)
        {
            this.name = name;
        }

        // Token: 0x060003D2 RID: 978 RVA: 0x00014314 File Offset: 0x00012514
        public static implicit operator string(Model model)
        {
            return model.name;
        }
    }
}