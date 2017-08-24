using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESI_ITE.Model
{
    public class ProgressReportModel
    {
        public float CurrentStage
        {
            get { return MyGlobals.ProgressCurrentStage; }
            set { MyGlobals.ProgressCurrentStage = value; }
        }

        public float PercentComplete
        {
            get { return MyGlobals.ProgressPercentComplete; }
            set { MyGlobals.ProgressPercentComplete = value; }
        }

        public float CurrentStep
        {
            get { return MyGlobals.ProgressCurrentStep; }
            set { MyGlobals.ProgressCurrentStep = value; }
        }

        public float TotalStep
        {
            get { return MyGlobals.ProgressTotalStep; }
            set { MyGlobals.ProgressTotalStep = value; }
        }

        public string Description
        {
            get { return MyGlobals.ProgressDescription; }
            set { MyGlobals.ProgressDescription = value; }
        }

    }
}
