using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    // Card properties
    public enum Faction {
        Common,
        Enlightened,
        Lifebound,
        Mechana,
        Void,
        //Monster
    }
    public int initNum;
    public GameManager GameManager;
    private bool isDragging;
    private Vector2 startPosition;
    private GameObject collidedContainer;
    protected GameObject parentContainer;
    protected Player player; // Reference to player that owns this card (null = not owned by anyone)

    void Awake() {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetParentContainer(GameObject parentContainer) {
        this.parentContainer = parentContainer;
    }

    public void SetPlayer(Player player) {
        this.player = player;
    }

    public void OnBeginDrag(PointerEventData data) {
        startPosition = transform.position;
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData data) {
        isDragging = false;
        if (collidedContainer == null) {
            transform.position = new Vector3(startPosition.x, startPosition.y, 0);
        } else {
            parentContainer.GetComponent<Container>().Remove(this.gameObject);
            collidedContainer.GetComponent<Container>().Add(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log($"Collided with {collision.gameObject.name}");
        collidedContainer = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        //Debug.Log($"Uncollided with {collision.gameObject.name}");
        if (collidedContainer == collision.gameObject) {
            collidedContainer = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging) {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            transform.position = new Vector3(objPosition.x, objPosition.y, 0);
        }
    }
}
