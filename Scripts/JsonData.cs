using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace AssemblyCSharp
{
    [Serializable]
    public class PlayerModel
    {
        public Model[] models;
        public PlayerModel(int count)
        {
            models = new Model[count];
        }

        [Serializable]
        public class Model
        {
            public List<int> uncomplete;
            public List<int> complete;
        }

        public static Model[] GetModelFromJson(string response)
        {
            PlayerModel model = JsonUtility.FromJson<PlayerModel>(response);
            return model.models;
        }

        public static string GetJsonFromModel(PlayerModel model, bool pretty)
        {
            return JsonUtility.ToJson(model, pretty);
        }
    }
}
