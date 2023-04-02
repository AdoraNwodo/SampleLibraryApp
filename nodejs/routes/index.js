var express = require('express');
const { v4: uuidv4 } = require('uuid');

var router = express.Router();

// Create an array to store books
let books = [
  { id: '1', title: 'The Great Gatsby', author: 'F. Scott Fitzgerald', borrowed: false },
  { id: '2', title: 'To Kill a Mockingbird', author: 'Harper Lee', borrowed: false },
  { id: '3', title: '1984', author: 'George Orwell', borrowed: false },
];

/* GET home page. */
router.get('/', function(req, res, next) {
  res.render('index', { title: 'Test Library App' });
});

// Get all books
router.get('/books', (req, res) => {
  res.send(books);
});

// Get a specific book by ID
router.get('/books/:id', (req, res) => {
  const book = books.find(b => b.id === req.params.id);
  if (book) {
    res.send(book);
  } else {
    res.status(404).send('Book not found');
  }
});

// Borrow a book by ID
router.put('/books/:id/borrow', (req, res) => {
  const book = books.find(b => b.id === req.params.id);
  if (book) {
    if (book.borrowed) {
      res.status(400).send('Book is already borrowed');
    } else {
      book.borrowed = true;
      res.send('Book successfully borrowed');
    }
  } else {
    res.status(404).send('Book not found');
  }
});

// Return a book by ID
router.put('/books/:id/return', (req, res) => {
  const book = books.find(b => b.id === req.params.id);
  if (book) {
    if (book.borrowed) {
      book.borrowed = false;
      res.send('Book successfully returned');
    } else {
      res.status(400).send('Book is not currently borrowed');
    }
  } else {
    res.status(404).send('Book not found');
  }
});

// Add a new book
router.post('/books/title/:title/author/:author', (req, res) => {
  const title = req.params.title;
  const author = req.params.author;
  if (!title || !author) {
    res.status(400).send('Missing required fields');
  } else {
    const newBook = {
      id: uuidv4(),
      title,
      author,
      borrowed: false,
    };
    books.push(newBook);
    res.send('Book successfully added');
  }
});

module.exports = router;
