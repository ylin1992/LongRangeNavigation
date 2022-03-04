using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using AegisLongRangeNavigationSimplified.Graphs;
using AegisLongRangeNavigationSimplified.Vertices;
using AegisLongRangeNavigationSimplified.Edges;
namespace AegisLongRangeNavigationSimplified.Parsers
{
	public class JSON3DParser
	{
		/**
		 * _file should not be null
		 * graphData should not be null
		 */

		
		private string _vertexFile;
		private string _edgeFile;
		private List<VertexData> _vertexDatas;
		private List<EdgeData> _edgeDatas;
		private Dictionary<int, Vertex3D> _vertices; // <Index, Vertex>
		public JSON3DParser(string vertexFile, string edgeFile)
		{
			_vertexFile = vertexFile;
			_edgeFile = edgeFile;
			_vertexDatas = new List<VertexData>();
			_edgeDatas = new List<EdgeData>();
			_vertices = new Dictionary<int, Vertex3D>();
			ReadVertex();
			ReadEdges();
		}

		private void ReadVertex()
		{
			Console.WriteLine("Reading json data from: " + _vertexFile);
			using (StreamReader r = new StreamReader(_vertexFile))
			{
				string json = r.ReadToEnd();
				_vertexDatas = JsonConvert.DeserializeObject<List<VertexData>>(json);
				Console.WriteLine("Number of vertices: " + _vertexDatas.Count);
			}
			foreach (VertexData graphData in _vertexDatas)
			{
				_vertices.Add(graphData.Index, new Vertex3D(graphData.Index,
					new double[] { graphData.X, graphData.Y, graphData.Z }));
			}
		}

		private void ReadEdges()
		{
			Console.WriteLine("Reading json data from: " + _edgeFile);
			using (StreamReader r = new StreamReader(_edgeFile))
			{
				string json = r.ReadToEnd();
				_edgeDatas = JsonConvert.DeserializeObject<List<EdgeData>>(json);
				Console.WriteLine("Number of edges: " + _edgeDatas.Count);
			}
		}

		public UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>> CreateWieghtedUndirectedGraph()
		{
			UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>> graph = new UndirectedWeightedGraph<Vertex3D, WeightedEdge<Vertex3D>>();

			Console.WriteLine("Constructing graph by data files...");
			foreach (EdgeData edge in _edgeDatas)
			{
				Vertex3D from = _vertices[edge.From];
				Vertex3D to = _vertices[edge.To];
				graph.AddEdge(new WeightedEdge<Vertex3D>(from, to, edge.Weight));
			}

			return graph;
		}

	}

	public class VertexData
	{
		public int Index;
		public double X;
		public double Y;
		public double Z;
	}

	public class EdgeData
	{
		public int From;
		public int To;
		public double Weight;
	}
}

