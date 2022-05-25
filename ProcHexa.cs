using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcHexa : MonoBehaviour
{
	public const float outerRadius = 10f;
	public const float innerRadius = outerRadius * 0.866025404f;
	private const float vyska = 1f;

	public Material mat;

	Renderer rend;
	private void Awake()
	{
		

		

		List<Vector3> points = new List<Vector3>(){
		new Vector3(0f, vyska, outerRadius),
		new Vector3(innerRadius, vyska, 0.5f * outerRadius),
		new Vector3(innerRadius, vyska, -0.5f * outerRadius),
		new Vector3(0f, vyska, -outerRadius),
		new Vector3(-innerRadius, vyska, -0.5f * outerRadius),
		new Vector3(-innerRadius, vyska, 0.5f * outerRadius),

		new Vector3(0f, 0f, outerRadius),
		new Vector3(innerRadius, 0f, 0.5f * outerRadius),
		new Vector3(innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(0f, 0f, -outerRadius),
		new Vector3(-innerRadius, 0f, -0.5f * outerRadius),
		new Vector3(-innerRadius, 0f, 0.5f * outerRadius)
		};

		int[] hexaIndices = new int[]
		{
			4,2,3,
			4,1,2,
			5,1,4,
			5,0,1,
			7,0,6,
			7,1,0,
			8,1,7,
			8,2,1,
			9,2,8,
			9,3,2,
			10,3,9,
			10,4,3,
			11,4,10,
			11,5,4,
			6,5,11,
			6,0,5,
			6,11,7,
			7,11,8,
			8,11,10,
			8,10,9
		};

		Mesh mesh = new Mesh();
		mesh.name = "Proc hexa";

		mesh.SetVertices(points);
		mesh.triangles = hexaIndices;

		mesh.RecalculateNormals();

		GetComponent<MeshFilter>().sharedMesh = mesh;
		rend = GetComponent<Renderer>();
	}

	public void setColor(Color clr)
	{
		rend.material.color = clr;
	}

	//private void Update()
	//{
	//	Renderer rend = GetComponent<Renderer>();

	//	rend.enabled = true;

	//	float vyska = this.transform.localScale[1];
	//	//mat.color = new Color(vyska,vyska,vyska);
	//	mat.color = new Color(1, 0, 0);
	//	rend.material = mat;
	//}

}
