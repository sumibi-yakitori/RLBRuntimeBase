using System.Linq;
using UniRx;
using UnityEngine;
using SY.Utils;
using UnityEngine.Serialization;

namespace RLB {
  [
    DisallowMultipleComponent
    , ExecuteInEditMode
    , SelectionBase
  ]
  public partial class ScatterComponent : MonoBehaviour /*, IRLBEmbeddable*/ {
    ////// Near-Constructors //////

    ////// Props //////
    [FormerlySerializedAs("prefab"), SerializeField]
    private GameObject original = null;
    
    [SerializeField] private uint amount = 10;
    [SerializeField] private Vector3 size = Vector3.one;

    ////// Methods //////
    protected void OnValidate() => this.Generate();
    // protected void OnEnable() => this.Generate();
    // protected void OnDisable() => this.Clear();

    private void Clear() {
      foreach (var child in this.transform.GetChildrenIncludeInactive().ToArray()) {
        Object.DestroyImmediate(child.gameObject);
      }
    }

    private void Generate() {
      if (this.original == null) { return; }

      this.gameObject.name = "Scatter: [" + this.original.name + "]";
      // var boxCollider = this.GetComponent<BoxCollider>();
      // boxCollider.enabled = false;

      Observable.NextFrame().Subscribe(_ => {
        this.Clear();

        var pos = Vector3.zero;
        foreach (var __ in Enumerable.Range(0, (int)this.amount)) {
          var go = Object.Instantiate(this.original, this.transform, false);
          var halfSize = this.size * 0.5f;
          pos.x = Random.Range(-halfSize.x, halfSize.x);
          pos.y = Random.Range(-halfSize.y, halfSize.y);
          pos.z = Random.Range(-halfSize.z, halfSize.z);
          go.transform.localPosition = pos;
        }
      });
    }
  }
}