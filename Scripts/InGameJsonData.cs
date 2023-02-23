using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp
{
    [Serializable]
    public class InGameModel
    {
        public Model[] models;
        public InGameModel (int count)
        {
            models = new Model[count];
        }

        [Serializable]
        public class Model 
        {
            public int money;
            public List<int> cookerUpgrade;
            public List<int> customerUpgrade;
            public List<int> incomeUpgrade;
            public List<int> speedUpgrade;
            public float starMeter;
        }

        public static Model[] GetModelFromJson(string respone)
        {
            InGameModel model = JsonUtility.FromJson<InGameModel>(respone);
            return model.models;
        }

        public static string GetJsonFromModel(InGameModel model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}
