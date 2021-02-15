using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMatrixManager {

    bool[][] matrix { get; set; }
    bool[][] toBeErased { get; set; }
    Color[][] colorMatrix { get; set; }

    GameObject[][] objectMatrix { get; set; }

    void UpdateMatrix();
    bool AddShape(IShape shape, int posX, int posY);
    bool MoreMoovesPossible(IShapeManager shapeManager);
}
