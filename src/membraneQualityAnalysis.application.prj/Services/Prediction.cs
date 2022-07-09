using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace membraneQualityAnalysis.application.Services;

public class Prediction
{
    public Box Box { get; set; }
    public string Label { get; set; }
    public float Confidence { get; set; }
}

public class Box
{
    public float Xmin { get; set; }
    public float Ymin { get; set; }
    public float Xmax { get; set; }
    public float Ymax { get; set; }

    public Box(float xmin, float ymin, float xmax, float ymax)
    {
        Xmin = xmin;
        Ymin = ymin;
        Xmax = xmax;
        Ymax = ymax;

    }
}
