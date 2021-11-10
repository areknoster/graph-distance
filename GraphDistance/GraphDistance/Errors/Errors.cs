namespace GraphDistance
{
    public static class Errors
    {
        public static class AdjacencyMatrix
        {
            public static string DIMENSIONS_NOT_EQUAL = "Adjacency matrix dimensions are not equal.";
            public static string INVALID_NODE_LABEL = "Invalid node label.";
            public static string NEGATIVE_SIZE = "Adjacency matrix size cannot be negative.";
        }

        public static class Graph
        {
            public static string INVALIDE_GRAPH_SIZE_OR_MATRIX = "Invalid graph size or adjacency matrix.";
            public static string NEGATIVE_SIZE = "Graph size cannot be negative.";
            public static string SUBGRAPH_CREATING_INVALID_NODE_LABEL = "Creating subraph: invalid node label detected.";
            public static string SUBGRAPH_CREATING_NODE_LABELS_NOT_UNIQUE = "Creating subraph: node labels are not unique.";
        }

        public static class GraphFile
        {
            public static string CANNOT_READ_MATRIX = "Cannot read adjacency matrix elements.";
            public static string CANNOT_READ_SIZE = "Cannot read graph size.";
            public static string FILE_EXTENSION_NOT_TXT = "File extension is not '.txt'.";
            public static string INVALID_PATH = "Invalid file path.";
            public static string MATRIX_NOT_BINARY = "Graph adjacency matrix is not binary matrix.";
            public static string MATRIX_NOT_SQUARE = "Graph adjacency matrix is not square matrix.";
        }
    }
}
