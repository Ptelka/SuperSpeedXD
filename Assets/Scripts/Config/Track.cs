using System;
using System.Collections.Generic;
using System.Linq;

namespace Config {
    [Serializable]
    public class Track {
        public List<Section> sections = new List<Section>();

        public float CalculateLength()
        {
            return sections.Sum(x => x.lenght);
        }

        public Section FindSection(float distance)
        {
            float sum = 0;
            distance = distance % CalculateLength();

            foreach (var section in sections)
            {
                sum += section.lenght;
                if (sum > distance)
                {
                    return section;
                }
            }
            
            return null;
        }

        [Serializable]
        public class Section {
            public float lenght;
            public float curvature;
        }
    }
}
