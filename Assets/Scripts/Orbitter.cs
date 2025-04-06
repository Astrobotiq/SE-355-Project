using UnityEngine;

public class Orbitter : MonoBehaviour
{
    public Transform target;     // Dönülecek hedef obje
        public float orbitSpeed = 50f; // Dönme hızı (derece/saniye)
        public float radius = 2f;    // Merkezden uzaklık (yarıçap)
    
        private float angle;
    
        void Update()
        {
            if (target == null) return;
    
            angle += orbitSpeed * Time.deltaTime;
            float rad = angle * Mathf.Deg2Rad;
    
            // Yeni pozisyonu hesapla
            float x = target.position.x + Mathf.Cos(rad) * radius;
            float y = target.position.y + Mathf.Sin(rad) * radius;
    
            transform.position = new Vector3(x, y, transform.position.z);
        }
}
