using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    private Mesh mesh;
    float fov;
    float angulo;
    Vector3 origin;
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

    }

    private void Update()
    {
        fov = 90f;
        origin = Vector3.zero;
        int numAristas = 50;
        angulo = 0f;
        float incremendoAngulo = fov / numAristas;
        float distanciaVision = 10f;

        Vector3[] vertices = new Vector3[numAristas + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangulos = new int[numAristas * 3];

        vertices[0] = origin;

        int indiceVertices = 1;
        int indiceTriangulos = 0;

        for (int i = 0; i <= numAristas; i++)
        {
            Vector3 vertex;
            RaycastHit2D rc2D = Physics2D.Raycast(origin, GetVectorFromAngle(angulo), distanciaVision);

            if (rc2D.collider != null)
            {
                vertex = origin + GetVectorFromAngle(angulo) * distanciaVision;
            }
            else
            {
                vertex = rc2D.point;
            }
            vertices[indiceVertices] = vertex;

            if (i > 0)
            {
                triangulos[indiceTriangulos + 0] = 0;
                triangulos[indiceTriangulos + 1] = indiceVertices - 1;
                triangulos[indiceTriangulos + 2] = indiceVertices;

                indiceTriangulos += 3;
            }

            indiceVertices++;
            angulo -= incremendoAngulo;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangulos;

        mesh.RecalculateBounds();
 
    }

    public void setOrigin(Vector3 newOrigin)
    {
        origin  = newOrigin;
    }

    public void setAimDirection(Vector3 AimDirection)
    {
        angulo = GetAngleFromVectorFloat(AimDirection) - fov / 2f;
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
