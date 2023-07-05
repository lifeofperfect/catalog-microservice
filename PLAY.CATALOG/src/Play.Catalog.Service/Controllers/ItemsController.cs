using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;

namespace Play.Catalog.Service.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        // private static readonly List<ItemDto> items = new()
        // {
        //     new ItemDto(Guid.NewGuid(), "Portion", "Restores a small amount of hp", 5, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Bronze sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Mirror", "Teleports you to a new universe", 7, DateTimeOffset.UtcNow)
        // };

        private readonly IRepository<Item> itemsRepository;
        public ItemsController(IRepository<Item> itemsRepository)
        {
            this.itemsRepository = itemsRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<ItemDto>> Items()
        {
            var items = (await itemsRepository.GetAllAsync()).Select(item => item.AsDto());

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById(Guid id)
        {
            //var item = items.Where(item => item.Id == id).SingleOrDefault();
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return item.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItem)
        {
            // var item = new ItemDto(Guid.NewGuid(), createItem.Name, createItem.Description, createItem.Price, DateTimeOffset.UtcNow);
            // items.Add(item);

            var item = new Item
            {
                Name = createItem.Name,
                Description = createItem.Description,
                Price = createItem.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };

            await itemsRepository.CreateAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateItemDto update)
        {
            //var item = items.Where(x => x.Id == id).SingleOrDefault();
            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            // var updatedItem = item with
            // {
            //     Name = update.Name,
            //     Description = update.Description,
            //     Price = update.Price,

            // };
            // var index = items.FindIndex(item => item.Id == id);
            // items[index] = updatedItem;

            item.Name = update.Name;
            item.Description = update.Description;
            item.Price = update.Price;

            await itemsRepository.UpdateAsync(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            // var index = items.FindIndex(x => x.Id == id);

            // if (index < 0)
            // {
            //     return NotFound();
            // }
            // items.RemoveAt(index);

            var item = await itemsRepository.GetAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            await itemsRepository.RemoveAsync(item.Id);

            return NoContent();
        }
    }
}