# PowerUpCollection

A collection of power-ups.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello

**Inheritance hierarchy:**

- Object
- ReadOnlyCollection&lt;IPowerUp&gt;
- ReadOnlyPowerUpCollection
- PowerUpCollection

## Methods

### Task DisablePowerUp(IPowerUp powerUp, CancellationToken ct)

Disables a power-up for a board.

**Parameter:** powerUp

The power-up to disble.

**Parameter:** ct

(Optional) A cancellation token for async processing.

### Task EnablePowerUp(IPowerUp powerUp, CancellationToken ct)

Enables a power-up for a board.

**Parameter:** powerUp

The power-up to enable.

**Parameter:** ct

(Optional) A cancellation token for async processing.

