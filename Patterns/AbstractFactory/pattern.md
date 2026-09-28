# Abstract Factory

The Abstract Factory is a creational design pattern that lets you produce families of related objects (or products) without specifying their concrete classes. It is particularly useful when your system needs to be independent of how its products are created, composed, and represented.


# Diagram
```mermaid
classDiagram
    class IAbstractFactory {
        <<interface>>
        +CreateProductA() IAbstractProductA
        +CreateProductB() IAbstractProductB
    }

    class ConcreteFactory1 {
        +CreateProductA() IAbstractProductA
        +CreateProductB() IAbstractProductB
    }

    class ConcreteFactory2 {
        +CreateProductA() IAbstractProductA
        +CreateProductB() IAbstractProductB
    }

    class IAbstractProductA {
        <<interface>>
        +OperationA()
    }

    class IAbstractProductB {
        <<interface>>
        +OperationB()
    }

    class ConcreteProductA1 {
        +OperationA()
    }

    class ConcreteProductB1 {
        +OperationB()
    }

    class ConcreteProductA2 {
        +OperationA()
    }

    class ConcreteProductB2 {
        +OperationB()
    }

    IAbstractFactory <|.. ConcreteFactory1
    IAbstractFactory <|.. ConcreteFactory2

    IAbstractProductA <|.. ConcreteProductA1
    IAbstractProductA <|.. ConcreteProductA2

    IAbstractProductB <|.. ConcreteProductB1
    IAbstractProductB <|.. ConcreteProductB2

    ConcreteFactory1 ..> ConcreteProductA1 : creates
    ConcreteFactory1 ..> ConcreteProductB1 : creates
    ConcreteFactory2 ..> ConcreteProductA2 : creates
    ConcreteFactory2 ..> ConcreteProductB2 : creates
```