using System;
using AegisLongRangeNavigation.Graphs;
using AegisLongRangeNavigation.Edges;
using AegisLongRangeNavigation.Vertices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace AegisLongRangeNavigation.Parsers
{
	public class JSONParser<TVertex, TEdge> where TEdge : IEdge<TVertex>
	{
		/**
		 * _file should not be null
		 * graphData should not be null
		 */

		private string _vertexFile;
		private string _edgeFile;
		private List<VertexData> _vertexDatas;
		private List<EdgeData> _edgeDatas;
		private Dictionary<int, Vertex> _vertices; // <Index, Vertex>
		public JSONParser(string vertexFile, string edgeFile)
		{
			_vertexFile = vertexFile;
			_edgeFile = edgeFile;
			_vertexDatas = new List<VertexData>();
			_edgeDatas = new List<EdgeData>();
			_vertices = new Dictionary<int, Vertex>();
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
				_vertices.Add(graphData.Index, new Vertex(graphData.Index,
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

		public WeightedUndirectedGraph<WeightedEdge<Vertex>, Vertex> CreateWieghtedUndirectedGraph()
		{
            WeightedUndirectedGraph<WeightedEdge<Vertex>, Vertex> graph = new WeightedUndirectedGraph<WeightedEdge<Vertex>, Vertex>();
            Func<Vertex, Vertex, double, WeightedEdge<Vertex>> func = (f, t, w) => new WeightedEdge<Vertex>(f, t, w);
            EdgeFactory<Vertex, WeightedEdge<Vertex>> weightedEdgeFactory = new EdgeFactory<Vertex, WeightedEdge<Vertex>>();

			Console.WriteLine("Constructing graph by data files...");
			foreach (EdgeData edge in _edgeDatas)
			{
				Vertex from = _vertices[edge.From];
				Vertex to = _vertices[edge.To];
				graph.AddEdge(from, to, edge.Weight, weightedEdgeFactory.WeightedEdgeCreator);
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

