using UnityEngine;

public enum Effect{
	RESTAURA_HP,
	NAO_RECEBE_DANO,
	REDUZ_DANO_RECEBIDO,
	CRITICO,
	AUMENTA_DANO
}

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

	public Sprite _spriteInGame;
	public Sprite _spriteUI;
	public string _description;
	public Effect _effect;
}
