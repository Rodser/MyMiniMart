using UnityEngine;

namespace Data
{
    public static class DataExtension
    {
        public static Vector3Data AsVector3Data(this Vector3 vector)
        {
            return new Vector3Data(vector.x, vector.y, vector.z);
        }     
        
        public static Vector3 AsVector3(this Vector3Data vector)
        {
            return new Vector3(vector.X, vector.Y, vector.Z);
        }
    }
}