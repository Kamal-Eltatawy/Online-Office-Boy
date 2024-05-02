using Domain.Const;

namespace Online_Office_Boy.Helper
{
    public static class Permission
    {
        public static List<string> GenerateViewPermissions(Modules module)
        {
            return new List<string> { $"Permission.{module.ToString()}.View" };
        }

        public static List<string> GenerateViewPermissions(string module)
        {
            return new List<string> { $"Permission.{module}.View" };
        }
        public static List<string> GenerateEditUpdateDeletePermissions(Modules module)
        {
            string moduleString = module.ToString();
            return new List<string>
            {
                $"Permission.{moduleString}.View",
                $"Permission.{moduleString}.Create",
                $"Permission.{moduleString}.Update",
                $"Permission.{moduleString}.Delete"
            };
        }
        public static List<string> GenerateEditUpdateDeletePermissions(string module)
        {
            return new List<string>
            {
                $"Permission.{module}.View",
                $"Permission.{module}.Create",
                $"Permission.{module}.Update",
                $"Permission.{module}.Delete"
            };
        }
        public static List<string> GenerateAllPermissions(bool isView = false)
        {
            var allPermissions = new List<string>();
            var modules = Enum.GetValues(typeof(Modules));

            foreach (Modules module in modules)
            {
                switch (module)
                {
                    case Modules.Home:
                            allPermissions.AddRange(GenerateViewPermissions(module));
                        break;
                    default:
                        allPermissions.AddRange(GenerateEditUpdateDeletePermissions(module));
                        break;
                }
            }

            return allPermissions;
        }




    }
}
