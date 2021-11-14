namespace GraphDistance.GraphDistanceTests.Algorithms
{
    public static class IntMatrixExtensions
    {
        public static int[,] SwapLabels(this int[,] matrix, int n1, int n2)
        {
            var newMatrix = matrix.Clone() as int[,];

            for (int i = 0; i < newMatrix.GetLength(0); i++)
            {
                int tmp = newMatrix[i, n1];
                newMatrix[i, n1] = newMatrix[i, n2];
                newMatrix[i, n2] = tmp;
            }

            for (int i = 0; i < newMatrix.GetLength(1); i++)
            {
                int tmp = newMatrix[n1, i];
                newMatrix[n1, i] = newMatrix[n2, i];
                newMatrix[n2, i] = tmp;
            }

            return newMatrix;
        }
    }
}