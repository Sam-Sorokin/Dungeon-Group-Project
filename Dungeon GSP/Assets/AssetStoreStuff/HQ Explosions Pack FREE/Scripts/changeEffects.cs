using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EffectsSystemFree
{
    public class changeEffects : MonoBehaviour
    {
        public GameObject[] effects;
        private GameObject currentObject;
        private int currentObjectID = 0;
        public GameObject guiTextLink;

        // Start is called before the first frame update
        void Start()
        {
            currentObject = Instantiate(effects[currentObjectID]);
            guiTextLink.GetComponent<Text>().text = effects[currentObjectID].name;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
                NextEffect();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PrevEffect();
        }

        public void NextEffect()
        {
            if (currentObjectID < effects.Length - 1)
            {
                Destroy(currentObject);
                currentObject = Instantiate(effects[++currentObjectID]);
                guiTextLink.GetComponent<Text>().text = effects[currentObjectID].name;

            }
        }

        public void PrevEffect()
        {
            if (currentObjectID > 0)
            {
                Destroy(currentObject);
                currentObject = Instantiate(effects[--currentObjectID]);
                guiTextLink.GetComponent<Text>().text = effects[currentObjectID].name;
            }
        }

        public void RefreshEffect()
        {
            Destroy(currentObject);
            currentObject = Instantiate(effects[currentObjectID]);
        }
    }
}