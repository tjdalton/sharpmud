namespace SharpMud.Commands
{
    public class East : Move,ICommand
    {
        private readonly string[] _accessWords = { "EAST", "E" };
        public string[] AccessWords
        {
            get { return _accessWords; }
        }


        public string Help
        {
            get { return "EAST"; }
        }


        public Permission[] PermissionsRequired
        {
            get
            {
                Permission[] p = { World.Db.Permissions.GetByName("none"), World.Db.Permissions.GetByName("all") };
                return p;
            }
        }
    }
}
