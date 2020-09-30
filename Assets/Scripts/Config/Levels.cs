using System;
using System.Collections.Generic;

namespace Config {
    
    [Serializable]
    public class Levels {
        public List<Level> levels = new List<Level>();
        
        [Serializable]
        public class Level {
            public string scene;
            public Track track;
        }
    }
}