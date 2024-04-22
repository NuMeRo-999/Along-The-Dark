using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float fov;
    public int numAristas = 50;
    public float anguloInicial;
    public float distanciaVision = 10f;
    public LayerMask layerMask;

    private Mesh mesh;
    private Vector3 origen;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 90f;
        origen = Vector3.zero;
    }

    void LateUpdate()
    {
        generarMesh();
    }

    private void generarMesh()
    {
        Vector3[] vertices = new Vector3[numAristas + 1 + 1];
        int[] triangulos = new int[numAristas * 3];

        float anguloActual = anguloInicial;
        float incrementoAngulo = fov / numAristas;

        vertices[0] = origen;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i <= numAristas; i++)
        {
            //0. Castear raycasts
            RaycastHit2D rc2D = Physics2D.Raycast(origen , GetVectorFromAngle(anguloActual), distanciaVision, layerMask);

            //1. Calcular la posición del nuevo vértice.
            Vector3 verticeActual;

            if(rc2D.collider == null) {
                verticeActual = origen + GetVectorFromAngle(anguloActual) * distanciaVision;
            }else {
                verticeActual = rc2D.point;
            }

            //2. Guardar el vértice en el array de vértices.
            vertices[indiceVertices] = verticeActual;

            //3. Guardar los indices en el array de triángulos.
            //Si no es la primera iteración.
            if (i != 0) {

                triangulos[indiceTriangulos] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;
                 
                indiceTriangulos += 3;
            }

            //4. Actualizar los índices de los arrays.
            indiceVertices++;
            anguloActual -= incrementoAngulo;

        }

        mesh.vertices = vertices;
        mesh.triangles = triangulos;
        mesh.RecalculateBounds();
    }

    public void setOrigin(Vector3 origin) {
        origen = origin;
    }

    public void setAimDirection(Vector3 AimDirection)
    {
        anguloInicial = GetAngleFromVectorFloat(AimDirection) - fov / 2f;
    }
    
    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }
}
