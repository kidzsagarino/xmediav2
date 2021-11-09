
class LinkedListNode {
    constructor(obj) {

        this.value = obj
        this.next = null
    }
}

class LinkedList {
    constructor(value) {
        const newNode = new LinkedListNode(value)



        if (!value) {
            this.head = null
            this.tail = null
            this.length = 0
        }
        else {
            this.head = newNode
            this.tail = this.head
            this.length = 1
        }

    }

    push(value) {
        const newNode = new LinkedListNode(value)

        if (!this.head) {
            this.head = newNode
            this.tail = newNode
        }
        else {
            this.tail.next = newNode
            this.tail = newNode
        }

        if (!this.length) {
            this.length = 1
        }
        else {
            this.length++
        }

        //return this
    }

    pop() {
        if (!this.head) return undefined

        let temp = this.head
        let pre = this.head

        while (temp.next) {
            pre = temp
            temp = temp.next
        }

        this.tail = pre
        this.tail.next = null
        this.length--

        if (this.length === 0) {
            this.head = null
            this.tail = null
        }

        return temp
    }

    unshift(value) {

        const newNode = new LinkedListNode(value)

        if (!this.head) {
            this.head = newNode
            this.tail = newNode
        }
        else {
            newNode.next = this.head
            this.head = newNode
        }
        this.length++
        return this
    }

    shift() {

        if (!this.head) return undefined

        let temp = this.head
        this.head = this.head.next
        temp.next = null

        this.length--

        if (this.length === 0) {
            this.head = null
            this.tail = null
        }

        return temp

    }

    get(index) {

        if (index < 0 || index >= this.length) return undefined

        let temp = this.head

        for (let i = 0; i < index; i++) {
            temp = temp.next
        }

        return temp

    }

    set(index, value) {

        if (index < 0 || index >= this.length) return undefined

        let temp = this.get(index)

        if (temp) {
            temp.value = value
            return true
        }
        return false
    }

    insert(index, value) {
        if (index === 0) return this.unshift(value)
        if (index === this.length) return this.push(value)
        if (index < 0 || index > this.length) return false

        const newNode = new LinkedListNode(value)
        const temp = this.get(index - 1)

        newNode.next = temp.next
        temp.next = newNode

        this.length++

        return true
    }

    remove(index) {
        if (index === 0) return this.shift()
        if (index === this.length - 1) return this.pop()
        if (index < 0 || index >= this.length) return undefined

        const temp = this.get(index)
        const pre = this.get(index - 1)

        pre.next = temp.next
        temp.next = null

        this.length--

        return temp

    }

    reverse() {
        let temp = this.head
        this.head = this.tail
        this.tail = temp
        let next = temp.next
        let prev = null

        for (let i = 0; i < this.length; i++) {
            next = temp.next
            temp.next = prev
            prev = temp
            temp = next
        }

        return this
    }

    getByID(id) {
        if (this.head == null) return undefined;

        let temp = this.head;

        if (id > 0) {

            while (temp.value.ID != id) {
                temp = temp.next;
            }

            return temp.value;
        }

        return null;
    }

    searchByIndex(propertyName, value) {
        
        if (this.head == null) return undefined

        let arr = [];
        let temp = this.head;

        if (value) {

            for (let i = temp; i != null; i = i.next) {

                if (i.value[propertyName].toUpperCase().indexOf(value.toUpperCase()) > -1) {
                    arr.push(i.value);
                }

            }
        }

        return arr;
    }
}