using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Dequeue from an empty queue
    // Expected Result: Throws InvalidOperationException with message "The queue is empty."
    // Defect(s) Found: None - this passed as the exception was correctly implemented.
    public void TestPriorityQueue_EmptyDequeue()
    {
        var priorityQueue = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue(), "The queue is empty.");
    }

    [TestMethod]
    // Scenario: Enqueue one item and dequeue it
    // Expected Result: Returns the enqueued value
    // Defect(s) Found: None - passed, but initially failed due to not removing item, but since only one, second dequeue not tested.
    public void TestPriorityQueue_SingleItem()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with different priorities, higher priority first
    // Expected Result: Dequeue returns higher priority item first
    // Defect(s) Found: Failed initially because the loop didn't check the last index (index < count-1 instead of < count), so always dequeued the first item.
    public void TestPriorityQueue_DifferentPriorities()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 2);
        Assert.AreEqual("B", priorityQueue.Dequeue()); // Higher priority
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue two items with same priority
    // Expected Result: Dequeue returns the first enqueued (FIFO)
    // Defect(s) Found: Failed on second dequeue because item was not removed after first dequeue, so second dequeue returned the same item again.
    public void TestPriorityQueue_SamePriority()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 1);
        Assert.AreEqual("A", priorityQueue.Dequeue()); // First in
        Assert.AreEqual("B", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items, mix priorities
    // Expected Result: Dequeue in priority order, FIFO for ties
    // Defect(s) Found: Failed on second dequeue due to not removing item; also loop bug prevented correct priority selection in some cases.
    public void TestPriorityQueue_MultipleItems()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 3);
        priorityQueue.Enqueue("C", 2);
        priorityQueue.Enqueue("D", 3);
        Assert.AreEqual("B", priorityQueue.Dequeue()); // Priority 3, first
        Assert.AreEqual("D", priorityQueue.Dequeue()); // Priority 3, second
        Assert.AreEqual("C", priorityQueue.Dequeue()); // Priority 2
        Assert.AreEqual("A", priorityQueue.Dequeue()); // Priority 1
    }
}