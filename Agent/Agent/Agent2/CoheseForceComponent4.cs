﻿using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace Agent.Agent2
{
  public class CoheseForceComponent4 : BoidForceComponent
  {
    /// <summary>
    /// Initializes a new instance of the CoheseForceComponent class.
    /// </summary>
    public CoheseForceComponent4()
      : base("Cohese Force", "Cohese4",
          "Cohesion",
          "Agent", "Agent2")
    {
      this.visionRadiusMultiplier = 1.0;
    }

    /// <summary>
    /// Registers all the output parameters for this component.
    /// </summary>
    protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
    {
      // Use the pManager object to register your output parameters.
      // Output parameters do not have default values, but they too must have the correct access type.
      pManager.AddGenericParameter("Cohesion Force", "F", "Cohesion Force", GH_ParamAccess.item);

      // Sometimes you want to hide a specific parameter from the Rhino preview.
      // You can use the HideParameter() method as a quick way:
      //pManager.HideParameter(1);
    }

    protected override Vector3d calcForce(AgentType agent, List<AgentType> neighbors)
    {
      Vector3d sum = new Vector3d();
      int count = 0;

      foreach (AgentType neighbor in neighbors)
      {
        //Adding up all the others' location
        sum = Vector3d.Add(sum, new Vector3d(neighbor.RefPosition));
        //For an average, we need to keep track of how many boids
        //are in our vision.
        count++;
      }

      if (count > 0)
      {
        //We desire to go in that direction at maximum speed.
        sum = Vector3d.Divide(sum, count);
        sum = Util.Agent.seek(agent, sum);
      }
      //Seek the average location of our neighbors.
      return sum;
    }

    /// <summary>
    /// Provides an Icon for the component.
    /// </summary>
    protected override System.Drawing.Bitmap Icon
    {
      get
      {
        //You can add image files to your project resources and access them like this:
        // return Resources.IconForThisComponent;
        return Properties.Resources.icon_coheseForce;
      }
    }

    /// <summary>
    /// Gets the unique ID for this component. Do not change this ID after release.
    /// </summary>
    public override Guid ComponentGuid
    {
      get { return new Guid("{c8fd8532-661e-47f1-99c1-b8552bb83b73}"); }
    }
  }
}