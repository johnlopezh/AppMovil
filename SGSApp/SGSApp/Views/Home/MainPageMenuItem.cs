using System;

namespace SGSApp.Views.Home
{
    public class MainPageMenuItem
    {
        public MainPageMenuItem()
        {
            TargetType = typeof(MainPageDetail);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public Type TargetType { get; set; }
    }
}