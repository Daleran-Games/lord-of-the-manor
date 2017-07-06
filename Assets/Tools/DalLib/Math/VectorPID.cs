using UnityEngine;

namespace DaleranGames
{

    public class VectorPID
    {

        public float pFactor, iFactor, dFactor;

        private float integralF;
        private float lastErrorF;

        private Vector2 integralV2;
        private Vector2 lastErrorV2;

        private Vector3 integralV3;
        private Vector3 lastErrorV3;

        public VectorPID(float pFactor, float iFactor, float dFactor)
        {
            this.pFactor = pFactor;
            this.iFactor = iFactor;
            this.dFactor = dFactor;
        }

        public float Update(float currentError, float timeFrame)
        {
            integralF += currentError * timeFrame;
            var deriv = (currentError - lastErrorF) / timeFrame;
            lastErrorF = currentError;
            return currentError * pFactor + integralF * iFactor + deriv * dFactor;
        }

        public Vector2 Update(Vector2 currentError, float timeFrame)
        {
            integralV2 += currentError * timeFrame;
            var deriv = (currentError - lastErrorV2) / timeFrame;
            lastErrorV2 = currentError;
            return currentError * pFactor + integralV2 * iFactor + deriv * dFactor;
        }

        public Vector3 Update(Vector3 currentError, float timeFrame)
        {
            integralV3 += currentError * timeFrame;
            var deriv = (currentError - lastErrorV3) / timeFrame;
            lastErrorV3 = currentError;
            return currentError * pFactor + integralV3 * iFactor + deriv * dFactor;
        }


    }
}
