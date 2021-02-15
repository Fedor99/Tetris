using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITouchControl {

    Transform selectedPivotShape { get; set; }
    Vector2 pressedPosition { get; set; }
    Vector2 hitBlockColliderID { get; set; }
    I_ID hitObjectID { get; set; }
    bool pressed { get; set; }
    bool pressedOnCollider { get; set; }
    Vector3 TouchUpdate();
    Vector2 ScreenToWorld(Vector2 screenPos);
}
