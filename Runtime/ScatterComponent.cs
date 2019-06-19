using System.Linq;
using UniRx;
using UnityEngine;
using SY.Utils;

namespace RLB {
  [ExecuteInEditMode, SelectionBase, DisallowMultipleComponent, RequireComponent(typeof(BoxCollider))]
  public class ScatterComponent : MonoBehaviour {
    ////// Near-Constructors //////

    ////// Props //////
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private uint amount = 10;

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
      if (this.prefab == null) { return; }

      this.gameObject.name = "Scatter: [" + this.prefab.name + "]";
      var boxCollider = this.GetComponent<BoxCollider>();
      boxCollider.enabled = false;

      Observable.NextFrame().Subscribe(_ => {
        this.Clear();

        var pos = Vector3.zero;
        foreach (var __ in Enumerable.Range(0, (int) this.amount)) {
          var go = Object.Instantiate(this.prefab, this.transform, false);
          var size = boxCollider.size * 0.5f;
          pos.x = Random.Range(-size.x, size.x);
          pos.y = Random.Range(-size.y, size.y);
          pos.z = Random.Range(-size.z, size.z);
          go.transform.localPosition = pos;
        }
      });
    }
  }
}