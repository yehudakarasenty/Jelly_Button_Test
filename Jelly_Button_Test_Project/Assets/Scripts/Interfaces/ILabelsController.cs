using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILabelsController : IController
{
    void SetView(ILabelsView view);
}
