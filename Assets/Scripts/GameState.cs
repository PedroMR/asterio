using System;
using System.Collections;

[Serializable]
public class GameState
{
	public Minotaur minotaur;
	public Asterio asterio;

	public GameState ()
	{
		Init();
	}
	
	public void Init()
	{
		minotaur = new Minotaur();
		asterio = new Asterio();
	}

	public void Update()
	{
		minotaur.Update();
		asterio.Update();
	}

}

[Serializable]
public class Mob
{
	public string name = "(anon)";
	public float hp = 1.0f;
	public bool dead = false;

	virtual public void Update()
	{
		if (hp <= 0) dead = true;
	}
}

[Serializable]
public class Eater : Mob
{
	public float food = 1.0f;

	public bool isHungry() { return food <= 0.2f; }
	public bool isStarving() { return food <= 0.0f; }

	override public void Update()
	{
		base.Update();

		food = MathUtils.clamp01(food - 0.05f);

		if (isStarving()) {
			hp -= 0.05f;
		}
	}

	virtual public void Feed(float amount) {
		food += amount;
		food = MathUtils.clamp01(food + amount);
	}
}

public class Asterio : Eater
{
	public float love = 0.5f;
	
	public Asterio()
	{
		name = "Asterio";
	}

	override public void Update()
	{
		base.Update();

		if (isStarving())
		{
			love = MathUtils.clamp01(love - 0.05f);
		}
	}

	override public void Feed(float amount) {
		if (food < 0.5f)
			love = MathUtils.clamp01(love + 0.05f);

		base.Feed(amount);
	}
}

public class Minotaur : Eater
{
	public float rage = 0.0f;

	public Minotaur()
	{
		name = "The Minotaur";
	}

	override public void Update()
	{
		base.Update();

		if (isHungry())
			rage += 0.05f;
		else
			rage -= 0.05f;

		rage = MathUtils.clamp01(rage);
	}
}